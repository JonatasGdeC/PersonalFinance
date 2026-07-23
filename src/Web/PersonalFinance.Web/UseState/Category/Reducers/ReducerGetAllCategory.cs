using Fluxor;
using PersonalFinance.Web.UseState.Category.State;

namespace PersonalFinance.Web.UseState.Category.Reducers;
using static CategoryActions;

public class ReducerGetAllCategory
{
    [ReducerMethod(actionType: typeof(GetAllCategoriesAction))]
    public static CategoryListState ReduceGetAllCategories(CategoryListState state)
        => new() { IsLoading = true, Categories = state.Categories };

    [ReducerMethod]
    public static CategoryListState ReduceGetAllCategoriesSuccess(CategoryListState state, GetAllCategoriesSuccessAction action)
        => new() { IsLoading = false, Categories = action.Categories };
}