using JobApp.Domain.FCG.Users.Domain.Abstractions;
using JobApp.Domain.Interface;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobApp.Domain.Models
{
    public class Applicant : BaseEntity, IAuditableEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Skill { get; set; } = string.Empty;
        public Guid UserId { get; set; }

        public DateTimeOffset CreatedAtUtc { get; set; }
        public DateTimeOffset? UpdatedAtUtc { get; set; }
        public string CreatedBy { get; set; } = null!;
        public string? UpdatedBy { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
    }
}
