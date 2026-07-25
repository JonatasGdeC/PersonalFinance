using Fluxor;
using PersonalFinance.Web.UseState.Participant.State;

namespace PersonalFinance.Web.UseState.Participant.Reducers;
using static ParticipantActions;

public class ReducerRegisterParticipant
{
    [ReducerMethod]
    public static ParticipantListState ReduceRegisterParticipantSuccess(ParticipantListState state, RegisterParticipantSuccessAction action)
        => new() { IsLoading = false, Participants = [..state.Participants, action.Participant] };
}
