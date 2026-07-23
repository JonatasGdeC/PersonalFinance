namespace PersonalFinance.Communication.Requests.Budget;

public record RegisterBudgetRequest
{
    public double MaximumSpend { get; set; }
    public required string Color { get; set; }
    public Guid CategoryId { get; set; }
}