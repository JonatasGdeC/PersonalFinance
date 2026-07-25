using Fluxor;
using PersonalFinance.Web.UseState.Participant.State;

namespace PersonalFinance.Web.UseState.Participant.Reducers;
using static ParticipantActions;

public class ReducerGetAllParticipant
{
    [ReducerMethod(actionType: typeof(GetAllParticipantsAction))]
    public static ParticipantListState ReduceGetAllParticipants(ParticipantListState state)
        => new() { IsLoading = true, Participants = state.Participants };

    [ReducerMethod]
    public static ParticipantListState ReduceGetAllParticipantsSuccess(ParticipantListState state, GetAllParticipantsSuccessAction action)
        => new() { IsLoading = false, Participants = action.Participants };
}
