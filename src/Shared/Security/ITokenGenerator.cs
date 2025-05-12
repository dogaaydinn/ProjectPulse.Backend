namespace Shared.Security;

public interface ITokenGenerator
{
    string GenerateAccessToken(Guid userId, string username, string role);
    string GenerateRefreshToken();
}