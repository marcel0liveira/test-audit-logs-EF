using JobApp.Application.Provider;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

// plano auditLog: criar
public class CurrentSessionProvider : ICurrentSessionProvider
{
    private readonly Guid? _currentUserId;

    public CurrentSessionProvider(IHttpContextAccessor accessor)
    {
        var userIdClaim = accessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
        var userId = userIdClaim?.Value;

        if (Guid.TryParse(userId, out var id))
            _currentUserId = id;
    }

    public Guid? GetUserId() => _currentUserId;
}
