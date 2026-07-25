using Fluxor;
using PersonalFinance.Communication.Dtos;

namespace PersonalFinance.Web.UseState.Participant.State;

[FeatureState]
public record ParticipantState
{
    public bool IsLoading { get; init; }
    public ParticipantDto? Participant { get; init; }
}
