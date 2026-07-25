using Fluxor;
using PersonalFinance.Web.UseState.Participant.State;

namespace PersonalFinance.Web.UseState.Participant.Reducers;
using static ParticipantActions;

public class ReducerUpdateParticipant
{
    [ReducerMethod]
    public static ParticipantListState ReduceUpdateParticipantSuccess(ParticipantListState state, UpdateParticipantSuccessAction action)
        => new()
        {
            IsLoading = false,
            Participants = state.Participants.Select(selector: p => p.Id == action.Participant.Id ? action.Participant : p).ToList()
        };

    [ReducerMethod]
    public static ParticipantState ReduceUpdateCurrentParticipantSuccess(ParticipantState state, UpdateParticipantSuccessAction action)
        => state.Participant?.Id == action.Participant.Id
            ? new() { IsLoading = false, Participant = action.Participant }
            : state;
}
