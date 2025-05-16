namespace Shared.Security.Authorization;

[AttributeUsage(AttributeTargets.Method)]
public class PermissionAttribute : AuthorizeAttribute
{
    public PermissionAttribute(string permission) : base(policy: permission) { }
}