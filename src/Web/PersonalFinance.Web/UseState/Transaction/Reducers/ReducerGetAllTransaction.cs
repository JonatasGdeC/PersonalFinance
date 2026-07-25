using Fluxor;
using PersonalFinance.Web.UseState.Transaction.State;

namespace PersonalFinance.Web.UseState.Transaction.Reducers;
using static TransactionActions;

public class ReducerGetAllTransaction
{
    [ReducerMethod(actionType: typeof(GetAllTransactionsAction))]
    public static TransactionListState ReduceGetAllTransactions(TransactionListState state)
        => new() { IsLoading = true, Transactions = state.Transactions };

    [ReducerMethod]
    public static TransactionListState ReduceGetAllTransactionsSuccess(TransactionListState state, GetAllTransactionsSuccessAction action)
        => new() { IsLoading = false, Transactions = action.Transactions };
}
