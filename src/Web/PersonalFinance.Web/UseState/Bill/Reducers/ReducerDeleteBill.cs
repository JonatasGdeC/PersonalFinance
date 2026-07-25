using Fluxor;
using PersonalFinance.Web.UseState.Bill.State;

namespace PersonalFinance.Web.UseState.Bill.Reducers;

using static BillActions;

public class ReducerDeleteBill
{
    [ReducerMethod]
    public static BillListState ReduceDeleteBillSuccess(BillListState state, DeleteBillSuccessAction action)
        => new() { IsLoading = false, Bills = state.Bills.Where(predicate: b => b.Id != action.BillId).ToList() };

    [ReducerMethod]
    public static BillState ReduceDeleteCurrentBillSuccess(BillState state, DeleteBillSuccessAction action)
        => state.Bill?.Id == action.BillId
            ? new() { IsLoading = false, Bill = null }
            : state;
}
