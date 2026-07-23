using Fluxor;
using PersonalFinance.Web.UseState.Category.State;

namespace PersonalFinance.Web.UseState.Category.Reducers;

using static CategoryActions;

public class ReducerDeleteCategory
{
    [ReducerMethod]
    public static CategoryListState ReduceDeleteCategorySuccess(CategoryListState state, DeleteCategorySuccessAction action)
        => new() { IsLoading = false, Categories = state.Categories.Where(predicate: b => b.Id != action.CategoryId).ToList() };

    [ReducerMethod]
    public static CategoryState ReduceDeleteCurrentCategorySuccess(CategoryState state, DeleteCategorySuccessAction action)
        => state.Category?.Id == action.CategoryId
            ? new() { IsLoading = false, Category = null }
            : state;
}