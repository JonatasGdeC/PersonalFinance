using Fluxor;
using PersonalFinance.Web.UseState.Bill.State;

namespace PersonalFinance.Web.UseState.Bill.Reducers;
using static BillActions;

public class ReducerUpdateBill
{
    [ReducerMethod]
    public static BillListState ReduceUpdateBillSuccess(BillListState state, UpdateBillSuccessAction action)
        => new()
        {
            IsLoading = false,
            Bills = state.Bills.Select(selector: b => b.Id == action.Bill.Id ? action.Bill : b).ToList()
        };

    [ReducerMethod]
    public static BillState ReduceUpdateCurrentBillSuccess(BillState state, UpdateBillSuccessAction action)
        => state.Bill?.Id == action.Bill.Id
            ? new() { IsLoading = false, Bill = action.Bill }
            : state;
}
