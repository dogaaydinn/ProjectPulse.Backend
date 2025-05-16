namespace Infrastructure.Security;

public interface IRefreshTokenService
{
    string GenerateRefreshToken();
    bool ValidateRefreshToken(string token);
}