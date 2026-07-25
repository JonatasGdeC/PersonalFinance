using Fluxor;
using PersonalFinance.Web.UseState.Budget.State;

namespace PersonalFinance.Web.UseState.Budget.Reducers;
using static BudgetActions;

public class ReducerUpdateBudget
{
    [ReducerMethod]
    public static BudgetListState ReduceUpdateBudgetSuccess(BudgetListState state, UpdateBudgetSuccessAction action)
        => new()
        {
            IsLoading = false,
            Budgets = state.Budgets.Select(selector: b => b.Id == action.Budget.Id ? action.Budget : b).ToList()
        };

    [ReducerMethod]
    public static BudgetState ReduceUpdateCurrentBudgetSuccess(BudgetState state, UpdateBudgetSuccessAction action)
        => state.Budget?.Id == action.Budget.Id
            ? new() { IsLoading = false, Budget = action.Budget }
            : state;
}
