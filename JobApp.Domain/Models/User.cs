using JobApp.Domain.FCG.Users.Domain.Abstractions;
using JobApp.Domain.Interfaces;

namespace JobApp.Domain.Models
{
    // plano auditLog: incluir Interface IAuditableEntity
    public class User : BaseEntity, IAuditableEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public DateTimeOffset CreatedAtUtc { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? UpdatedAtUtc { get; set; }
        public string CreatedBy { get; set; } = null!;
        public string? UpdatedBy { get; set; }
        public virtual ICollection<Applicant> Applicants { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
    }
}
