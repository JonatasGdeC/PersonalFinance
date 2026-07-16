using PersonalFinance.Domain.Security.Cryptography;
using BC = BCrypt.Net.BCrypt;

namespace PersonalFinance.Infrastructure.Security.Cryptography;

internal class BCrypt : IEncrypter
{
    public string Encrypt(string value) => BC.HashPassword(inputKey: value);

    public bool Verify(string value, string hash) => BC.Verify(text: value, hash: hash);
}