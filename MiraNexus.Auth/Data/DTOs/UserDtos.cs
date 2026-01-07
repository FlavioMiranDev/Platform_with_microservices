namespace MiraNexus.Auth.Data.DTOs;

public record UserLoginRequestDTO(
    string Email,
    string Password
);

public class UserLoginResponseDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
}

public record UserRegisterRequestDTO(
    string Name,
    string Doc,
    string Email,
    string Password,
    string VerifyPassword,
    string? Phone
);

public class UserRegisterResponseDTO
{
    public string Message { get; set; }
}

public record UserActiveRequestDTO(Guid Id, string Email, string Code);

public record UserLogoutRequestDTO(Guid Id, string Token);

public class UserCacheDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Doc { get; set; }
    public string IsActive { get; set; }
}

public record UserCreatedEvent
{
    public Guid Id { get; init; }
    public string Email { get; init; } = string.Empty;
    public string Phone { get; init; } = string.Empty;
    public string Token { get; init; } = string.Empty;
}
