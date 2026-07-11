namespace PersonalFinance.Communication.Dtos;

public record CategoryDto
{
    public long Id { get; init; }
    public required string Name { get; init; }
}