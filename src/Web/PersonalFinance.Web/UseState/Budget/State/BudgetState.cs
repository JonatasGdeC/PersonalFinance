using Fluxor;
using PersonalFinance.Communication.Dtos;

namespace PersonalFinance.Web.UseState.Budget.State;

[FeatureState]
public record BudgetState
{
    public bool IsLoading { get; init; }
    public BudgetDto? Budget { get; init; }
}
