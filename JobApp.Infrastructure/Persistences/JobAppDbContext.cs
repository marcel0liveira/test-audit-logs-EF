using JobApp.Application.Provider;
using JobApp.Domain.Enums;
using JobApp.Domain.Interfaces;
using JobApp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace JobApp.Infrastructure.Persistences
{
    public class JobAppDbContext : DbContext
    {
        // plano auditLog: criar
        private readonly ICurrentSessionProvider _sessionProvider;

        public DbSet<Applicant> Applicants => Set<Applicant>();
        public DbSet<Company> Companies => Set<Company>();
        public DbSet<JobPost> JobPosts => Set<JobPost>();
        public DbSet<User> Users => Set<User>();
        public DbSet<AuditTrail> AuditTrails => Set<AuditTrail>();

        // plano auditLog: alterar
        public JobAppDbContext(DbContextOptions<JobAppDbContext> options, ICurrentSessionProvider sessionProvider) : base(options)
        {
            _sessionProvider = sessionProvider;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(JobAppDbContext).Assembly);
        }

        // plano auditLog: criar
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var userId = _sessionProvider.GetUserId();

            SetAuditableProperties(userId);
            var auditEntries = HandleAuditingBeforeSaveChanges(userId);

            if (auditEntries.Any())
                await AuditTrails.AddRangeAsync(auditEntries, cancellationToken);

            return await base.SaveChangesAsync(cancellationToken);
        }

        // plano auditLog: criar
        private void SetAuditableProperties(Guid? userId)
        {
            const string system = "system";
            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAtUtc = DateTimeOffset.UtcNow;
                    entry.Entity.CreatedBy = userId?.ToString() ?? system;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAtUtc = DateTimeOffset.UtcNow;
                    entry.Entity.UpdatedBy = userId?.ToString() ?? system;
                }
            }
        }

        // plano auditLog: criar
        private List<AuditTrail> HandleAuditingBeforeSaveChanges(Guid? userId)
        {
            var entries = ChangeTracker.Entries<IAuditableEntity>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted);

            var auditTrails = new List<AuditTrail>();

            foreach (var entry in entries)
            {
                var audit = new AuditTrail
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    EntityName = entry.Entity.GetType().Name,
                    DateUtc = DateTimeOffset.UtcNow
                };

                foreach (var prop in entry.Properties)
                {
                    if (prop.Metadata.IsPrimaryKey())
                    {
                        audit.PrimaryKey = prop.CurrentValue?.ToString();
                        continue;
                    }

                    if (prop.Metadata.Name.Equals("PasswordHash")) continue;

                    var name = prop.Metadata.Name;

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            audit.TrailType = TrailType.Create;
                            audit.NewValues[name] = prop.CurrentValue;
                            break;
                        case EntityState.Deleted:
                            audit.TrailType = TrailType.Delete;
                            audit.OldValues[name] = prop.OriginalValue;
                            break;
                        case EntityState.Modified:
                            if (!Equals(prop.OriginalValue, prop.CurrentValue))
                            {
                                audit.TrailType = TrailType.Update;
                                audit.ChangedColumns.Add(name);
                                audit.OldValues[name] = prop.OriginalValue;
                                audit.NewValues[name] = prop.CurrentValue;
                            }
                            break;
                    }
                }

                if (audit.TrailType != TrailType.None)
                    auditTrails.Add(audit);
            }

            return auditTrails;
        }
    }
}
