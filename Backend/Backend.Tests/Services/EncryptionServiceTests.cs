using Backend.Infrastructure.Services;
using FluentAssertions;
using Microsoft.Extensions.Configuration;

namespace Backend.Tests.Services;

public class EncryptionServiceTests
{
    private static EncryptionService CreateService(string? key = null)
    {
        var dict = new Dictionary<string, string?>();
        if (key != null) dict["Encryption:Key"] = key;

        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(dict)
            .Build();

        return new EncryptionService(config);
    }

    [Fact]
    public void Encrypt_ValidText_ReturnsEncryptedString()
    {
        var service = CreateService();
        var encrypted = service.Encrypt("Hello, World!");

        encrypted.Should().NotBeNullOrEmpty();
        encrypted.Should().NotBe("Hello, World!");
    }

    [Fact]
    public void Encrypt_SameTextTwice_ReturnsSameString()
    {
        var service = CreateService();
        var enc1 = service.Encrypt("Hello");
        var enc2 = service.Encrypt("Hello");

        // AES с фиксированным IV — детерминированный
        enc1.Should().Be(enc2);
    }

    [Fact]
    public void Encrypt_EmptyText_ReturnsEmpty()
    {
        var service = CreateService();
        service.Encrypt("").Should().Be("");
    }

    [Fact]
    public void Encrypt_NullText_ReturnsNull()
    {
        var service = CreateService();
        service.Encrypt(null!).Should().BeNull();
    }

    [Fact]
    public void Decrypt_EncryptedText_ReturnsOriginal()
    {
        var service = CreateService();
        var original = "Hello, World!";
        var encrypted = service.Encrypt(original);
        var decrypted = service.Decrypt(encrypted);

        decrypted.Should().Be(original);
    }

    [Fact]
    public void Decrypt_CyrillicText_RoundTrips()
    {
        var service = CreateService();
        var original = "Привет, мир!";
        var decrypted = service.Decrypt(service.Encrypt(original));

        decrypted.Should().Be(original);
    }

    [Fact]
    public void Decrypt_LongText_RoundTrips()
    {
        var service = CreateService();
        var original = new string('a', 1000);
        var decrypted = service.Decrypt(service.Encrypt(original));

        decrypted.Should().Be(original);
    }

    [Fact]
    public void Decrypt_EmptyText_ReturnsEmpty()
    {
        var service = CreateService();
        service.Decrypt("").Should().Be("");
    }

    [Fact]
    public void Decrypt_InvalidBase64_ReturnsOriginal()
    {
        var service = CreateService();
        var invalid = "not-a-base64-string!!!";
        service.Decrypt(invalid).Should().Be(invalid);
    }

    [Fact]
    public void EncryptIfNotEmpty_ValidText_ReturnsEncrypted()
    {
        var service = CreateService();
        var result = service.EncryptIfNotEmpty("Hello");

        result.Should().NotBeNullOrEmpty();
        result.Should().NotBe("Hello");
    }

    [Fact]
    public void EncryptIfNotEmpty_EmptyText_ReturnsNull()
    {
        var service = CreateService();
        service.EncryptIfNotEmpty("").Should().BeNull();
        service.EncryptIfNotEmpty("   ").Should().BeNull();
        service.EncryptIfNotEmpty(null).Should().BeNull();
    }

    [Fact]
    public void DecryptIfNotEmpty_ValidText_ReturnsDecrypted()
    {
        var service = CreateService();
        var encrypted = service.Encrypt("Hello");
        service.DecryptIfNotEmpty(encrypted).Should().Be("Hello");
    }

    [Fact]
    public void DecryptIfNotEmpty_EmptyText_ReturnsNull()
    {
        var service = CreateService();
        service.DecryptIfNotEmpty("").Should().BeNull();
        service.DecryptIfNotEmpty(null).Should().BeNull();
    }

    [Fact]
    public void EncryptWithCustomKey_DifferentFromDefault()
    {
        var service1 = CreateService("MyCustomKey12345678901234567890");
        var service2 = CreateService();

        var enc1 = service1.Encrypt("Hello");
        var enc2 = service2.Encrypt("Hello");

        enc1.Should().NotBe(enc2);
    }
}