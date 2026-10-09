using Backend.Infrastructure.Services;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace Backend.Tests.Services;

public class JwtServiceTests
{
    private static IConfiguration CreateConfig(
        string? secretKey = null,
        string? issuer = null,
        string? audience = null)
    {
        var dict = new Dictionary<string, string?>();
        if (secretKey != null) dict["Jwt:SecretKey"] = secretKey;
        if (issuer != null) dict["Jwt:Issuer"] = issuer;
        if (audience != null) dict["Jwt:Audience"] = audience;

        return new ConfigurationBuilder()
            .AddInMemoryCollection(dict)
            .Build();
    }

    // === ГЕНЕРАЦИЯ ТОКЕНА ===

    [Fact]
    public void GenerateToken_ValidData_ReturnsToken()
    {
        var config = CreateConfig(
            secretKey: "this-is-a-super-secret-key-32-characters!",
            issuer: "TestIssuer",
            audience: "TestAudience");

        var service = new JwtService(config);
        var token = service.GenerateToken(1, "testuser", "User");

        token.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void GenerateToken_TokenIsValidJwt()
    {
        var config = CreateConfig(secretKey: "this-is-a-super-secret-key-32-characters!");
        var service = new JwtService(config);

        var token = service.GenerateToken(1, "testuser", "User");

        var handler = new JwtSecurityTokenHandler();
        handler.CanReadToken(token).Should().BeTrue();
    }

    [Fact]
    public void GenerateToken_ContainsUserIdClaim()
    {
        var config = CreateConfig(secretKey: "this-is-a-super-secret-key-32-characters!");
        var service = new JwtService(config);

        var token = service.GenerateToken(42, "testuser", "Admin");
        var handler = new JwtSecurityTokenHandler();
        var parsed = handler.ReadJwtToken(token);

        parsed.Claims.Should().Contain(c => c.Type == "userId" && c.Value == "42");
    }

    [Fact]
    public void GenerateToken_ContainsUserNameClaim()
    {
        var config = CreateConfig(secretKey: "this-is-a-super-secret-key-32-characters!");
        var service = new JwtService(config);

        var token = service.GenerateToken(1, "testuser", "User");
        var parsed = new JwtSecurityTokenHandler().ReadJwtToken(token);

        parsed.Claims.Should().Contain(c => c.Type == "userName" && c.Value == "testuser");
    }

    [Fact]
    public void GenerateToken_ContainsRoleClaim()
    {
        var config = CreateConfig(secretKey: "this-is-a-super-secret-key-32-characters!");
        var service = new JwtService(config);

        var token = service.GenerateToken(1, "admin", "Admin");
        var parsed = new JwtSecurityTokenHandler().ReadJwtToken(token);

        parsed.Claims.Should().Contain(c => c.Type == "role" && c.Value == "Admin");
    }

    [Fact]
    public void GenerateToken_UsesConfiguredIssuer()
    {
        var config = CreateConfig(
            secretKey: "this-is-a-super-secret-key-32-characters!",
            issuer: "MyIssuer",
            audience: "MyAudience");

        var service = new JwtService(config);
        var token = service.GenerateToken(1, "user", "User");
        var parsed = new JwtSecurityTokenHandler().ReadJwtToken(token);

        parsed.Issuer.Should().Be("MyIssuer");
        parsed.Audiences.Should().Contain("MyAudience");
    }

    [Fact]
    public void GenerateToken_NoConfig_UsesDefaults()
    {
        var config = CreateConfig(); // пустая
        var service = new JwtService(config);
        var token = service.GenerateToken(1, "user", "User");
        var parsed = new JwtSecurityTokenHandler().ReadJwtToken(token);

        parsed.Issuer.Should().Be("GlanceVexAPI");
        parsed.Audiences.Should().Contain("GlanceVexClient");
    }

    [Fact]
    public void GenerateToken_ExpiresInAboutSevenDays()
    {
        var config = CreateConfig(secretKey: "this-is-a-super-secret-key-32-characters!");
        var service = new JwtService(config);
        var before = DateTime.UtcNow;

        var token = service.GenerateToken(1, "user", "User");
        var parsed = new JwtSecurityTokenHandler().ReadJwtToken(token);

        var expires = parsed.ValidTo;
        var diff = expires - before;
        diff.TotalDays.Should().BeApproximately(7, 0.1);
    }
}