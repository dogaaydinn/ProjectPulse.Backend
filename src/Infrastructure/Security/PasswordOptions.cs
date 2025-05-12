using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Infrastructure.Security;

public class PasswordOptions
{
    public int SaltSize { get; init; } = 16;
    public int HashSize { get; init; } = 32;
    public int Iterations { get; init; } = 100_000;
    public KeyDerivationPrf Algorithm { get; init; } = KeyDerivationPrf.HMACSHA256;
}