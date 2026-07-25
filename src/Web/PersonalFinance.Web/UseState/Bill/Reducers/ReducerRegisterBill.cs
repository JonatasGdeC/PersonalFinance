using Fluxor;
using PersonalFinance.Web.UseState.Bill.State;

namespace PersonalFinance.Web.UseState.Bill.Reducers;
using static BillActions;

public class ReducerRegisterBill
{
    [ReducerMethod]
    public static BillListState ReduceRegisterBillSuccess(BillListState state, RegisterBillSuccessAction action)
        => new() { IsLoading = false, Bills = [..state.Bills, action.Bill] };
}
