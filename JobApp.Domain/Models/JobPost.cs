using JobApp.Domain.FCG.Users.Domain.Abstractions;
using JobApp.Domain.Interfaces;

namespace JobApp.Domain.Models
{
    // plano auditLog: incluir Interface IAuditableEntity
    public class JobPost : BaseEntity, IAuditableEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid CompanyId { get; set; }
        public Company Company { get; set; } = null!;

        public DateTimeOffset CreatedAtUtc { get; set; }
        public DateTimeOffset? UpdatedAtUtc { get; set; }
        public string CreatedBy { get; set; } = null!;
        public string? UpdatedBy { get; set; }
    }
}
