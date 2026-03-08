namespace JobApp.Domain.Interface
{
    public interface IAuditableEntity
    {
        public DateTimeOffset CreatedAt { get; init; }
        public DateTimeOffset? UpdatedAt { get; set; }

        public string CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
