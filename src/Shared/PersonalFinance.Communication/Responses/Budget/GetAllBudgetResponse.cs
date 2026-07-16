using PersonalFinance.Communication.Dtos;

namespace PersonalFinance.Communication.Responses.Budget;

public record GetAllBudgetResponse
{
    public List<BudgetDto> ListBudgets { get; init; } = [];
}