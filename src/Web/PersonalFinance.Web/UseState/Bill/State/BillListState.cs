using Fluxor;
using PersonalFinance.Communication.Dtos;

namespace PersonalFinance.Web.UseState.Bill.State;

[FeatureState]
public record BillListState
{
    public bool IsLoading { get; init; }
    public List<BillDto> Bills { get; init; } = [];
}
