using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Backend.Domain.Shared;

namespace Backend.Infrastructure.Services;

public class EncryptionService : IEncryptionService
{
    private readonly byte[] _key;
    private readonly byte[] _iv;

    public EncryptionService(IConfiguration configuration)
    {
        var keyString = configuration["Encryption:Key"] 
            ?? "MySuperSecretKey1234567890123456";

        _key = Encoding.UTF8.GetBytes(keyString.PadRight(32).Substring(0, 32));

        _iv = Encoding.UTF8.GetBytes("1234567890123456");
    }

    public string Encrypt(string plainText)
    {
        if (string.IsNullOrEmpty(plainText))
            return plainText;

        using var aes = Aes.Create();
        aes.Key = _key;
        aes.IV = _iv;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;

        using var encryptor = aes.CreateEncryptor();
        using var ms = new MemoryStream();
        using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
        using (var sw = new StreamWriter(cs))
        {
            sw.Write(plainText);
        }

        return Convert.ToBase64String(ms.ToArray());
    }

    public string Decrypt(string cipherText)
    {
        if (string.IsNullOrEmpty(cipherText))
            return cipherText;

        try
        {
            var bytes = Convert.FromBase64String(cipherText);

            using var aes = Aes.Create();
            aes.Key = _key;
            aes.IV = _iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using var decryptor = aes.CreateDecryptor();
            using var ms = new MemoryStream(bytes);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);

            return sr.ReadToEnd();
        }
        catch
        {
            // Если не удалось расшифровать — возвращаем исходное
            return cipherText;
        }
    }

    public string? EncryptIfNotEmpty(string? plainText)
    {
        if (string.IsNullOrWhiteSpace(plainText))
            return null;

        return Encrypt(plainText);
    }

    public string? DecryptIfNotEmpty(string? cipherText)
    {
        if (string.IsNullOrWhiteSpace(cipherText))
            return null;

        return Decrypt(cipherText);
    }
}