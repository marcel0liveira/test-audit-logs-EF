using JobApp.Domain.Enums;

namespace JobApp.Domain.Models
{
    public class AuditTrail
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public string EntityName { get; set; } = null!;
        public string? PrimaryKey { get; set; }
        public TrailType TrailType { get; set; }
        public DateTimeOffset DateUtc { get; set; }

        public Dictionary<string, object?> OldValues { get; set; } = [];
        public Dictionary<string, object?> NewValues { get; set; } = [];
        public List<string> ChangedColumns { get; set; } = [];
    }
}
