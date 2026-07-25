using Fluxor;
using PersonalFinance.Web.UseState.Transaction.State;

namespace PersonalFinance.Web.UseState.Transaction.Reducers;
using static TransactionActions;

public class ReducerUpdateTransaction
{
    [ReducerMethod]
    public static TransactionListState ReduceUpdateTransactionSuccess(TransactionListState state, UpdateTransactionSuccessAction action)
        => new()
        {
            IsLoading = false,
            Transactions = state.Transactions.Select(selector: t => t.Id == action.Transaction.Id ? action.Transaction : t).ToList()
        };

    [ReducerMethod]
    public static TransactionState ReduceUpdateCurrentTransactionSuccess(TransactionState state, UpdateTransactionSuccessAction action)
        => state.Transaction?.Id == action.Transaction.Id
            ? new() { IsLoading = false, Transaction = action.Transaction }
            : state;
}
