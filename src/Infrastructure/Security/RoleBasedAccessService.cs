using Microsoft.AspNetCore.Http;

namespace Infrastructure.Security;

public class RoleBasedAccessService : IPermissionService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RoleBasedAccessService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    [CacheResponse(Duration = 30)]
    public bool HasPermission(Guid userId, string permission)
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null) return false;

        var claims = user.FindAll("permissions").Select(c => c.Value);
        return claims.Contains(permission);
    }
}