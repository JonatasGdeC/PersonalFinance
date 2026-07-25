using Fluxor;
using PersonalFinance.Web.UseState.Budget.State;

namespace PersonalFinance.Web.UseState.Budget.Reducers;

using static BudgetActions;

public class ReducerDeleteBudget
{
    [ReducerMethod]
    public static BudgetListState ReduceDeleteBudgetSuccess(BudgetListState state, DeleteBudgetSuccessAction action)
        => new() { IsLoading = false, Budgets = state.Budgets.Where(predicate: b => b.Id != action.BudgetId).ToList() };

    [ReducerMethod]
    public static BudgetState ReduceDeleteCurrentBudgetSuccess(BudgetState state, DeleteBudgetSuccessAction action)
        => state.Budget?.Id == action.BudgetId
            ? new() { IsLoading = false, Budget = null }
            : state;
}
