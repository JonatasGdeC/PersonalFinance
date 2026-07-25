using Fluxor;
using PersonalFinance.Web.UseState.Transaction.State;

namespace PersonalFinance.Web.UseState.Transaction.Reducers;

using static TransactionActions;

public class ReducerDeleteTransaction
{
    [ReducerMethod]
    public static TransactionListState ReduceDeleteTransactionSuccess(TransactionListState state, DeleteTransactionSuccessAction action)
        => new() { IsLoading = false, Transactions = state.Transactions.Where(predicate: t => t.Id != action.TransactionId).ToList() };

    [ReducerMethod]
    public static TransactionState ReduceDeleteCurrentTransactionSuccess(TransactionState state, DeleteTransactionSuccessAction action)
        => state.Transaction?.Id == action.TransactionId
            ? new() { IsLoading = false, Transaction = null }
            : state;
}
