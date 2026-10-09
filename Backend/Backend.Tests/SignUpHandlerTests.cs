using Backend.Application.Users.Commands;
using Backend.Application.Users.Handlers;
using Backend.Domain.Shared;
using Backend.Domain.Users;
using Backend.Domain.Users.Repositories;
using Backend.Domain.Users.ValueObjects;
using FluentAssertions;
using Moq;

namespace Backend.Tests.Handlers;

public class SignUpHandlerTests
{
    private readonly Mock<IUserRepository> _userRepo = new();
    private readonly Mock<IPasswordHasher> _passwordHasher = new();
    private readonly SignUpHandler _handler;

    public SignUpHandlerTests()
    {
        _handler = new SignUpHandler(_userRepo.Object, _passwordHasher.Object);
    }

    private static PasswordHash FakeHash() =>
        PasswordHash.FromHash(new string('a', 60));

    [Fact]
    public async Task Handle_ValidData_CreatesUser()
    {
        var command = new SignUpCommand
        {
            UserName = "newuser",
            Password = "password123",
            ConfirmPassword = "password123",
            Role = "User"
        };

        _userRepo
            .Setup(r => r.ExistsAsync(It.IsAny<UserName>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        _passwordHasher
            .Setup(h => h.Hash("password123"))
            .Returns(FakeHash());

        _userRepo
            .Setup(r => r.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().BeGreaterThanOrEqualTo(0);
        _userRepo.Verify(
            r => r.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_PasswordsDoNotMatch_ThrowsDomainException()
    {
        var command = new SignUpCommand
        {
            UserName = "newuser",
            Password = "password123",
            ConfirmPassword = "otherpassword",
            Role = "User"
        };

        var act = async () => await _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<DomainException>();
    }

    [Fact]
    public async Task Handle_UserAlreadyExists_ThrowsDomainException()
    {
        var command = new SignUpCommand
        {
            UserName = "existing",
            Password = "password123",
            ConfirmPassword = "password123",
            Role = "User"
        };

        _userRepo
            .Setup(r => r.ExistsAsync(It.IsAny<UserName>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var act = async () => await _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<DomainException>();
    }

    [Fact]
    public async Task Handle_InvalidRole_ThrowsDomainException()
    {
        var command = new SignUpCommand
        {
            UserName = "newuser",
            Password = "password123",
            ConfirmPassword = "password123",
            Role = "SuperAdmin" // невалидная роль
        };

        _userRepo
            .Setup(r => r.ExistsAsync(It.IsAny<UserName>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        _passwordHasher
            .Setup(h => h.Hash(It.IsAny<string>()))
            .Returns(FakeHash());

        var act = async () => await _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<DomainException>();
    }

    [Fact]
    public async Task Handle_ValidData_HashesPassword()
    {
        var command = new SignUpCommand
        {
            UserName = "newuser",
            Password = "password123",
            ConfirmPassword = "password123",
            Role = "User"
        };

        _userRepo
            .Setup(r => r.ExistsAsync(It.IsAny<UserName>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        _passwordHasher
            .Setup(h => h.Hash("password123"))
            .Returns(FakeHash());

        _userRepo
            .Setup(r => r.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        await _handler.Handle(command, CancellationToken.None);

        _passwordHasher.Verify(h => h.Hash("password123"), Times.Once);
    }

    [Fact]
    public async Task Handle_AdminRole_CreatesAdminUser()
    {
        var command = new SignUpCommand
        {
            UserName = "adminuser",
            Password = "password123",
            ConfirmPassword = "password123",
            Role = "Admin"
        };

        _userRepo
            .Setup(r => r.ExistsAsync(It.IsAny<UserName>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        _passwordHasher
            .Setup(h => h.Hash(It.IsAny<string>()))
            .Returns(FakeHash());

        User? capturedUser = null;
        _userRepo
            .Setup(r => r.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
            .Callback<User, CancellationToken>((u, _) => capturedUser = u)
            .Returns(Task.CompletedTask);

        await _handler.Handle(command, CancellationToken.None);

        capturedUser.Should().NotBeNull();
        capturedUser!.Role.Should().Be(Role.Admin);
    }
}