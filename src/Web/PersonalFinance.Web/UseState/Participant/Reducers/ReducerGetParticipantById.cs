using Fluxor;
using PersonalFinance.Web.UseState.Participant.State;

namespace PersonalFinance.Web.UseState.Participant.Reducers;
using static ParticipantActions;

public class ReducerGetParticipantById
{
    [ReducerMethod(actionType: typeof(GetParticipantByIdAction))]
    public static ParticipantState ReduceGetParticipantById(ParticipantState state)
        => new() { IsLoading = true, Participant = null };

    [ReducerMethod]
    public static ParticipantState ReduceGetParticipantByIdSuccess(ParticipantState state, GetParticipantByIdSuccessAction action)
        => new() { IsLoading = false, Participant = action.Participant };
}
