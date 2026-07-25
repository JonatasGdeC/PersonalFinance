using Fluxor;
using PersonalFinance.Web.UseState.Bill.State;

namespace PersonalFinance.Web.UseState.Bill.Reducers;
using static BillActions;

public class ReducerGetBillById
{
    [ReducerMethod(actionType: typeof(GetBillByIdAction))]
    public static BillState ReduceGetBillById(BillState state)
        => new() { IsLoading = true, Bill = null };

    [ReducerMethod]
    public static BillState ReduceGetBillByIdSuccess(BillState state, GetBillByIdSuccessAction action)
        => new() { IsLoading = false, Bill = action.Bill };
}
