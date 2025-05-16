namespace Infrastructure.Security;

public interface IPermissionService
{
    bool HasPermission(Guid userId, string permission);
}