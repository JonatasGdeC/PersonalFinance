using FinanceService.Domain.Enums;

namespace PersonalFinance.Communication.Dtos;

public record CategoryDto
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public required TransactionType Type  { get; init; }
}