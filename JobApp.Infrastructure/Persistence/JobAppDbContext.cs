using JobApp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace JobApp.Infrastructure.Persistence
{
    public class JobAppDbContext : DbContext
    {
        public DbSet<Applicant> Applicants => Set<Applicant>();
        public DbSet<Company> Companies => Set<Company>();
        public DbSet<JobPost> JobPosts => Set<JobPost>();
        public DbSet<User> Users => Set<User>();

        public JobAppDbContext(DbContextOptions<JobAppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(JobAppDbContext).Assembly);
        }
    }
}
