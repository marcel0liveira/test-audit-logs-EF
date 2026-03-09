namespace JobApp.Domain.Interfaces
{
    // plano auditLog: criar
    public interface IAuditableEntity
    {
        public DateTimeOffset CreatedAtUtc { get; set; }
        public DateTimeOffset? UpdatedAtUtc { get; set; }

        public string CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
