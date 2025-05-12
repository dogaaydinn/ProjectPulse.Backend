using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Shared.Security;

namespace Infrastructure.Security;

public class PasswordHasher : IUserPasswordHasher
{
    private readonly PasswordOptions _options;

    public PasswordHasher(PasswordOptions options)
    {
        _options = options;
    }

    public string HashPassword(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(_options.SaltSize);
        var hash = KeyDerivation.Pbkdf2(
            password,
            salt,
            _options.Algorithm,
            _options.Iterations,
            _options.HashSize
        );

        var combined = new byte[1 + salt.Length + hash.Length];
        combined[0] = 0x01;
        Buffer.BlockCopy(salt, 0, combined, 1, salt.Length);
        Buffer.BlockCopy(hash, 0, combined, 1 + salt.Length, hash.Length);

        return Convert.ToBase64String(combined);
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        var decoded = Convert.FromBase64String(hashedPassword);

        if (decoded[0] != 0x01) return false;

        var salt = new byte[_options.SaltSize];
        Buffer.BlockCopy(decoded, 1, salt, 0, salt.Length);

        var expectedHash = new byte[_options.HashSize];
        Buffer.BlockCopy(decoded, 1 + salt.Length, expectedHash, 0, expectedHash.Length);

        var actualHash = KeyDerivation.Pbkdf2(
            password,
            salt,
            _options.Algorithm,
            _options.Iterations,
            _options.HashSize
        );

        return CryptographicOperations.FixedTimeEquals(expectedHash, actualHash);
    }
}