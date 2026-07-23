namespace PersonalFinance.Communication.Dtos;

public record BudgetDto
{
    public Guid Id { get; init; }
    public double MaximumSpend { get; init; }
    public required string Color { get; init; }
    public required CategoryDto Category { get; init; }
}