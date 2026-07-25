using Fluxor;
using PersonalFinance.Web.UseState.Pot.State;

namespace PersonalFinance.Web.UseState.Pot.Reducers;

using static PotActions;

public class ReducerDeletePot
{
    [ReducerMethod]
    public static PotListState ReduceDeletePotSuccess(PotListState state, DeletePotSuccessAction action)
        => new() { IsLoading = false, Pots = state.Pots.Where(predicate: p => p.Id != action.PotId).ToList() };

    [ReducerMethod]
    public static PotState ReduceDeleteCurrentPotSuccess(PotState state, DeletePotSuccessAction action)
        => state.Pot?.Id == action.PotId
            ? new() { IsLoading = false, Pot = null }
            : state;
}
