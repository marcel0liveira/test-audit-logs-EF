namespace JobApp.Application.DTO
{
    public class JobPostDtoInp
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid CompanyId { get; set; }
    }
}
