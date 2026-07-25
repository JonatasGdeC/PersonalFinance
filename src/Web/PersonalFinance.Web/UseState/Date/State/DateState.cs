using Fluxor;

namespace PersonalFinance.Web.UseState.Date.State;

[FeatureState]
public record DateState
{
    public DateTime CurrentDate { get; init; } = DateTime.Today;
}
