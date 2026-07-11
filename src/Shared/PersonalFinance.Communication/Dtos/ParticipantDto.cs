namespace PersonalFinance.Communication.Dtos;

public record ParticipantDto
{
    public long Id { get; init; }
    public required string Name { get; init; }
    public string? Image { get; init; }
}