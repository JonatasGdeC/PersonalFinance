namespace PersonalFinance.Communication.Dtos;

public record PotDto
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public double CurrentAmount { get; init; }
    public double Target { get; init; }
    public required string Color { get; init; }
}