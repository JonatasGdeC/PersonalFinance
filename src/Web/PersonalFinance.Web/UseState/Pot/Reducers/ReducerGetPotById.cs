using Fluxor;
using PersonalFinance.Web.UseState.Pot.State;

namespace PersonalFinance.Web.UseState.Pot.Reducers;
using static PotActions;

public class ReducerGetPotById
{
    [ReducerMethod(actionType: typeof(GetPotByIdAction))]
    public static PotState ReduceGetPotById(PotState state)
        => new() { IsLoading = true, Pot = null };

    [ReducerMethod]
    public static PotState ReduceGetPotByIdSuccess(PotState state, GetPotByIdSuccessAction action)
        => new() { IsLoading = false, Pot = action.Pot };
}
