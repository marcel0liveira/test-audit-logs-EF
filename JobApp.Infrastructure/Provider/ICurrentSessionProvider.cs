namespace JobApp.Application.Provider
{
    // plano auditLog: criar
    public interface ICurrentSessionProvider
    {
        Guid? GetUserId();
    }
}
