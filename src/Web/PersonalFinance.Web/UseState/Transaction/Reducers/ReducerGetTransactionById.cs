using Fluxor;
using PersonalFinance.Web.UseState.Transaction.State;

namespace PersonalFinance.Web.UseState.Transaction.Reducers;
using static TransactionActions;

public class ReducerGetTransactionById
{
    [ReducerMethod(actionType: typeof(GetTransactionByIdAction))]
    public static TransactionState ReduceGetTransactionById(TransactionState state)
        => new() { IsLoading = true, Transaction = null };

    [ReducerMethod]
    public static TransactionState ReduceGetTransactionByIdSuccess(TransactionState state, GetTransactionByIdSuccessAction action)
        => new() { IsLoading = false, Transaction = action.Transaction };
}
