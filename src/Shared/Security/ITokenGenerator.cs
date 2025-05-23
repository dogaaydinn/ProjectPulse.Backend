using System.Security.Claims;

namespace Shared.Security;

public interface ITokenGenerator
{
    TokenResult GenerateAccessToken(Guid userId, string username, IEnumerable<string> roles);
    string GenerateRefreshToken();
    ClaimsPrincipal? GetPrincipalFromToken(string token);
}

public record TokenResult(
    string Token,
    DateTime Expiration,
    IEnumerable<string> Roles
);