namespace PersonalFinance.Communication.Dtos;

public record UserDto
{
    public required string Name { get; init; }
    public required string Email { get; init; }
    public string? ProfileImage { get; init; }
}