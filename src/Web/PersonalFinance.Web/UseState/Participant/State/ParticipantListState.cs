using Fluxor;
using PersonalFinance.Communication.Dtos;

namespace PersonalFinance.Web.UseState.Participant.State;

[FeatureState]
public record ParticipantListState
{
    public bool IsLoading { get; init; }
    public List<ParticipantDto> Participants { get; init; } = [];
}
