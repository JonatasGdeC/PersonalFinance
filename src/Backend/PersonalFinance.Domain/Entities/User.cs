namespace PersonalFinance.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public string? Password { get; set; }
    public string? GoogleId { get; set; } 
    public string? ProfileImage { get; set; }
}