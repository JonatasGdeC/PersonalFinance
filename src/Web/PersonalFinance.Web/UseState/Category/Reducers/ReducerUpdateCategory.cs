using Fluxor;
using PersonalFinance.Web.UseState.Category.State;

namespace PersonalFinance.Web.UseState.Category.Reducers;
using static CategoryActions;

public class ReducerUpdateCategory
{
    [ReducerMethod]
    public static CategoryListState ReduceUpdateCategorySuccess(CategoryListState state, UpdateCategorySuccessAction action)
        => new()
        {
            IsLoading = false,
            Categories = state.Categories.Select(selector: b => b.Id == action.Category.Id ? action.Category : b).ToList()
        };

    [ReducerMethod]
    public static CategoryState ReduceUpdateCurrentCategorySuccess(CategoryState state, UpdateCategorySuccessAction action)
        => state.Category?.Id == action.Category.Id
            ? new() { IsLoading = false, Category = action.Category }
            : state;
}