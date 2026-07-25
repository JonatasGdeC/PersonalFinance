using Fluxor;
using PersonalFinance.Communication.Dtos;

namespace PersonalFinance.Web.UseState.Bill.State;

[FeatureState]
public record BillState
{
    public bool IsLoading { get; init; }
    public BillDto? Bill { get; init; }
}
