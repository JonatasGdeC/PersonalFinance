using Fluxor;
using PersonalFinance.Communication.Dtos;

namespace PersonalFinance.Web.UseState.Pot.State;

[FeatureState]
public record PotListState
{
    public bool IsLoading { get; init; }
    public List<PotDto> Pots { get; init; } = [];
}
