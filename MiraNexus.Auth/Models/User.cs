namespace MiraNexus.Auth.Models;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string? Phone { get; set; }
    public string Doc { get; set; }
    public string? ValidationCode { get; set; }
    public DateTime? IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}
