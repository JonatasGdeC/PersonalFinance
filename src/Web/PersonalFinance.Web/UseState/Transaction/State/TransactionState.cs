using Fluxor;
using PersonalFinance.Communication.Dtos;

namespace PersonalFinance.Web.UseState.Transaction.State;

[FeatureState]
public record TransactionState
{
    public bool IsLoading { get; init; }
    public TransactionDto? Transaction { get; init; }
}
