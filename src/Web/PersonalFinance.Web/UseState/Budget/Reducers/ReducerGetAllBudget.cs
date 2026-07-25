using Fluxor;
using PersonalFinance.Web.UseState.Budget.State;

namespace PersonalFinance.Web.UseState.Budget.Reducers;
using static BudgetActions;

public class ReducerGetAllBudget
{
    [ReducerMethod(actionType: typeof(GetAllBudgetsAction))]
    public static BudgetListState ReduceGetAllBudgets(BudgetListState state)
        => new() { IsLoading = true, Budgets = state.Budgets };

    [ReducerMethod]
    public static BudgetListState ReduceGetAllBudgetsSuccess(BudgetListState state, GetAllBudgetsSuccessAction action)
        => new() { IsLoading = false, Budgets = action.Budgets };
}
