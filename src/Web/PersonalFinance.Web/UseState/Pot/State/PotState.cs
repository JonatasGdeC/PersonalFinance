using Fluxor;
using PersonalFinance.Communication.Dtos;

namespace PersonalFinance.Web.UseState.Pot.State;

[FeatureState]
public record PotState
{
    public bool IsLoading { get; init; }
    public PotDto? Pot { get; init; }
}
