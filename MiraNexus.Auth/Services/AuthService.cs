using MassTransit;
using Microsoft.AspNetCore.Identity;
using MiraNexus.Auth.Data.DTOs;
using MiraNexus.Auth.Models;
using MiraNexus.Auth.Repositories;

namespace MiraNexus.Auth.Services;

public class AuthService
{
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly UserRepository _userRepository;
    private readonly AuthRepository _authRepository;
    private readonly TokenService _tokenService;
    private readonly PasswordService _passwordService;
    private readonly ISendEndpointProvider _sendEndpointProvider;

    public AuthService(IPasswordHasher<User> passwordHasher,
            UserRepository userRepository,
            PasswordService passwordService,
            TokenService tokenService,
            AuthRepository authRepository,
            ISendEndpointProvider sendEndpointProvider)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _passwordService = passwordService;
        _tokenService = tokenService;
        _authRepository = authRepository;
        _sendEndpointProvider = sendEndpointProvider;
    }

    public async Task<UserLoginResponseDTO?> Authenticate(UserLoginRequestDTO login)
    {
        var user = await _userRepository.GetByEmailAsync(login.Email);

        if (user is null
        || user.IsActive is null
        || await _authRepository.Exists(user.Email)
        || _passwordHasher.VerifyHashedPassword(
                user,
                user.Password,
                login.Password
            ) == PasswordVerificationResult.Failed) return null;

        string token = _tokenService.GenerateToken();

        var userCache = new UserCacheDTO
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Phone = user.Phone ?? "",
            Doc = user.Doc,
            IsActive = user.IsActive.ToString() ?? ""
        };

        await _authRepository.SaveAsync(userCache, token);
        await _authRepository.SetLoged(user.Email);

        return new UserLoginResponseDTO
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Token = token
        };
    }

    public async Task Logout(string token, Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        string email = user?.Email.ToString() ?? "";

        if (await _authRepository.Exists(token) && await _authRepository.Exists(email))
        {
            await _authRepository.RemoveAsync(token);
            await _authRepository.RemoveAsync(email);
        }
    }

    public async Task<User?> Register(UserRegisterRequestDTO user)
    {
        if (
            await _userRepository.EmailExistsAsync(user.Email)
            || !_passwordService.IsValidPasswords(user.Password, user.VerifyPassword)
        ) return null;

        var userEntity = new User
        {
            Name = user.Name,
            Email = user.Email,
            Doc = user.Doc,
            IsActive = null,
            Phone = user.Phone,
            CreatedAt = DateTime.Now
        };

        var passwordHashed = _passwordHasher.HashPassword(userEntity, user.Password);

        userEntity.Password = passwordHashed;

        var newUser = await _userRepository.CreateAsync(userEntity);

        await NorifyUserCreated(newUser);

        return newUser;
    }

    public async Task<User?> Active(UserActiveRequestDTO token)
    {
        var user = await _userRepository.GetByEmailAsync(token.Email);

        if (
            user is null
            || user.ValidationCode != token.Code
            || user.Id != token.Id
        ) return null;

        user.ValidationCode = null;
        user.IsActive = DateTime.Now;

        return await _userRepository.UpdateAsync(user);
    }

    public async Task<UserLoginResponseDTO?> Refresh(string token)
    {
        var userToken = await _authRepository.GetAsync(token);

        if (userToken is null) return null;

        var userMapped = new UserLoginResponseDTO
        {
            Id = userToken.Id,
            Name = userToken.Name,
            Email = userToken.Email,
            Token = token
        };

        return userMapped;
    }

    private async Task NorifyUserCreated(User user)
    {
        var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:userCreatedqueue"));

        await endpoint.Send(new UserCreatedEvent
        {
            Id = user.Id,
            Email = user.Email,
            Phone = user.Phone ?? string.Empty,
            Token = user.ValidationCode ?? throw new InvalidOperationException()
        });
    }
}
