using Fluxor;
using PersonalFinance.Web.UseState.Pot.State;

namespace PersonalFinance.Web.UseState.Pot.Reducers;
using static PotActions;

public class ReducerGetAllPot
{
    [ReducerMethod(actionType: typeof(GetAllPotsAction))]
    public static PotListState ReduceGetAllPots(PotListState state)
        => new() { IsLoading = true, Pots = state.Pots };

    [ReducerMethod]
    public static PotListState ReduceGetAllPotsSuccess(PotListState state, GetAllPotsSuccessAction action)
        => new() { IsLoading = false, Pots = action.Pots };
}
