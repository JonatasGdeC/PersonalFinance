using Fluxor;
using PersonalFinance.Web.UseState.Budget.State;

namespace PersonalFinance.Web.UseState.Budget.Reducers;
using static BudgetActions;

public class ReducerRegisterBudget
{
    [ReducerMethod]
    public static BudgetListState ReduceRegisterBudgetSuccess(BudgetListState state, RegisterBudgetSuccessAction action)
        => new() { IsLoading = false, Budgets = [..state.Budgets, action.Budget] };
}
