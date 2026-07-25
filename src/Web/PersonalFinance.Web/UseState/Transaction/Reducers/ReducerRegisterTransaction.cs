using Fluxor;
using PersonalFinance.Web.UseState.Transaction.State;

namespace PersonalFinance.Web.UseState.Transaction.Reducers;
using static TransactionActions;

public class ReducerRegisterTransaction
{
    [ReducerMethod]
    public static TransactionListState ReduceRegisterTransactionSuccess(TransactionListState state, RegisterTransactionSuccessAction action)
        => new() { IsLoading = false, Transactions = [..state.Transactions, action.Transaction] };
}
