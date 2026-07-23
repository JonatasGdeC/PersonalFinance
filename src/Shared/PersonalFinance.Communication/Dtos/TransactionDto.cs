using PersonalFinance.Communication.Enums;

namespace PersonalFinance.Communication.Dtos;

public record TransactionDto
{
    public Guid Id { get; init; }
    public DateTime Date { get; init; }
    public TransactionType Type { get; init; }
    public double Amount { get; init; }
    public CategoryDto? Category { get; init; }
    public required ParticipantDto ParticipantDto { get; init; }
}