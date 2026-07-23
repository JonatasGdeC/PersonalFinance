using Fluxor;
using PersonalFinance.Communication.Dtos;

namespace PersonalFinance.Web.UseState.Category.State;

[FeatureState]
public record CategoryListState
{
    public bool IsLoading { get; init; }
    public List<CategoryDto> Categories { get; init; } = [];
}