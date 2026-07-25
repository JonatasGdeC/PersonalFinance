using Fluxor;
using PersonalFinance.Web.UseState.Budget.State;

namespace PersonalFinance.Web.UseState.Budget.Reducers;
using static BudgetActions;

public class ReducerGetBudgetById
{
    [ReducerMethod(actionType: typeof(GetBudgetByIdAction))]
    public static BudgetState ReduceGetBudgetById(BudgetState state)
        => new() { IsLoading = true, Budget = null };

    [ReducerMethod]
    public static BudgetState ReduceGetBudgetByIdSuccess(BudgetState state, GetBudgetByIdSuccessAction action)
        => new() { IsLoading = false, Budget = action.Budget };
}
