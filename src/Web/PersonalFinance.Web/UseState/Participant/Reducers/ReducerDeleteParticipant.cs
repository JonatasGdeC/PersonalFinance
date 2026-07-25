using Fluxor;
using PersonalFinance.Web.UseState.Participant.State;

namespace PersonalFinance.Web.UseState.Participant.Reducers;

using static ParticipantActions;

public class ReducerDeleteParticipant
{
    [ReducerMethod]
    public static ParticipantListState ReduceDeleteParticipantSuccess(ParticipantListState state, DeleteParticipantSuccessAction action)
        => new() { IsLoading = false, Participants = state.Participants.Where(predicate: p => p.Id != action.ParticipantId).ToList() };

    [ReducerMethod]
    public static ParticipantState ReduceDeleteCurrentParticipantSuccess(ParticipantState state, DeleteParticipantSuccessAction action)
        => state.Participant?.Id == action.ParticipantId
            ? new() { IsLoading = false, Participant = null }
            : state;
}
