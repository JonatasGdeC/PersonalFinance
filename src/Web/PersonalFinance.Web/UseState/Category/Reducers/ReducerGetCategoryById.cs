using Fluxor;
using PersonalFinance.Web.UseState.Category.State;

namespace PersonalFinance.Web.UseState.Category.Reducers;
using static CategoryActions;

public class ReducerGetCategoryById
{
    [ReducerMethod(actionType: typeof(GetCategoryByIdAction))]
    public static CategoryState ReduceGetCategoryById(CategoryState state)
        => new() { IsLoading = true, Category = null };

    [ReducerMethod]
    public static CategoryState ReduceGetCategoryByIdSuccess(CategoryState state, GetCategoryByIdSuccessAction action)
        => new() { IsLoading = false, Category = action.Category };
}