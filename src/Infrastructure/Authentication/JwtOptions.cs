namespace Infrastructure.Authentication;

public class JwtOptions
{
    public string SecretKey { get; init; } = null!;
    public string Issuer { get; init; } = "ProjectPulse";
    public string Audience { get; init; } = "ProjectPulseClient";
    public int AccessTokenExpirationMinutes { get; init; } = 30;
}