using Fluxor;
using PersonalFinance.Communication.Dtos;

namespace PersonalFinance.Web.UseState.Transaction.State;

[FeatureState]
public record TransactionListState
{
    public bool IsLoading { get; init; }
    public List<TransactionDto> Transactions { get; init; } = [];
}
