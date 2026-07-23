using Fluxor;
using PersonalFinance.Communication.Dtos;

namespace PersonalFinance.Web.UseState.Category.State;

[FeatureState]
public record CategoryState
{
    public bool IsLoading { get; init; }
    public CategoryDto? Category { get; init; }
}
