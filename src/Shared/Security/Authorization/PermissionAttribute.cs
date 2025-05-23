namespace Shared.Security.Authorization;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
public class PermissionAttribute : Attribute
{
    public string Permission { get; }

    public PermissionAttribute(string permission)
    {
        Permission = permission;
    }
    //TODO:API katmanında ise bu marker’ı okuyan bir IAuthorizationPolicyProvider ve bir AuthorizationHandlerayı implement edip, gerçek AuthorizeAttribute/policy bazlı yetkilendirmeyi orada yapabilirsiniz. Böylece Shared katmanı tamamen NuGet bağımsız kalır.
}