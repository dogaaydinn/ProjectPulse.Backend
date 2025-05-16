using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Security;

public class RefreshTokenService : IRefreshTokenService
{
    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public bool ValidateRefreshToken(string token)
    {
        return !string.IsNullOrWhiteSpace(token) && Base64UrlEncoder.CanDecode(token);
    }
}