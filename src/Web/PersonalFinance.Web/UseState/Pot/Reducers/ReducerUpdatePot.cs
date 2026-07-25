using Fluxor;
using PersonalFinance.Web.UseState.Pot.State;

namespace PersonalFinance.Web.UseState.Pot.Reducers;
using static PotActions;

public class ReducerUpdatePot
{
    [ReducerMethod]
    public static PotListState ReduceUpdatePotSuccess(PotListState state, UpdatePotSuccessAction action)
        => new()
        {
            IsLoading = false,
            Pots = state.Pots.Select(selector: p => p.Id == action.Pot.Id ? action.Pot : p).ToList()
        };

    [ReducerMethod]
    public static PotState ReduceUpdateCurrentPotSuccess(PotState state, UpdatePotSuccessAction action)
        => state.Pot?.Id == action.Pot.Id
            ? new() { IsLoading = false, Pot = action.Pot }
            : state;
}
