using Fluxor;
using PersonalFinance.Web.UseState.Pot.State;

namespace PersonalFinance.Web.UseState.Pot.Reducers;
using static PotActions;

public class ReducerRegisterPot
{
    [ReducerMethod]
    public static PotListState ReduceRegisterPotSuccess(PotListState state, RegisterPotSuccessAction action)
        => new() { IsLoading = false, Pots = [..state.Pots, action.Pot] };
}
