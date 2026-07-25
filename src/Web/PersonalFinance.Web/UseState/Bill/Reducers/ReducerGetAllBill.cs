using Fluxor;
using PersonalFinance.Web.UseState.Bill.State;

namespace PersonalFinance.Web.UseState.Bill.Reducers;
using static BillActions;

public class ReducerGetAllBill
{
    [ReducerMethod(actionType: typeof(GetAllBillsAction))]
    public static BillListState ReduceGetAllBills(BillListState state)
        => new() { IsLoading = true, Bills = state.Bills };

    [ReducerMethod]
    public static BillListState ReduceGetAllBillsSuccess(BillListState state, GetAllBillsSuccessAction action)
        => new() { IsLoading = false, Bills = action.Bills };
}
