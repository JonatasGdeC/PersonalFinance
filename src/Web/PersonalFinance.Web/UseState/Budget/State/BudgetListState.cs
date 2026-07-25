using Fluxor;
using PersonalFinance.Communication.Dtos;

namespace PersonalFinance.Web.UseState.Budget.State;

[FeatureState]
public record BudgetListState
{
    public bool IsLoading { get; init; }
    public List<BudgetDto> Budgets { get; init; } = [];
}
