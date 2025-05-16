using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Infrastructure.Security;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Authentication;

public sealed class RefreshTokenService : IRefreshTokenService
{
    private readonly JwtOptions _jwtOptions;

    public RefreshTokenService(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    public string GenerateRefreshToken()
    {
        var randomBytes = RandomNumberGenerator.GetBytes(64);
        return Convert.ToBase64String(randomBytes);
    }

    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, _jwtOptions.TokenValidationParameters(), out var securityToken);

        if (securityToken is not JwtSecurityToken jwt || !jwt.Header.Alg.Equals(_jwtOptions.Algorithm, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return principal;
    }

    // token validation
    public bool ValidateRefreshToken(string token)
    {
        try
        {
            var decoded = Base64UrlEncoder.Decode(token);
            return decoded.Length == 32; // 256-bit token validation
        }
        catch
        {
            return false;
        }
    }
}