using Fluxor;
using PersonalFinance.Web.UseState.Category.State;

namespace PersonalFinance.Web.UseState.Category.Reducers;
using static CategoryActions;

public class ReducerRegisterCategory
{
    [ReducerMethod]
    public static CategoryListState ReduceRegisterBoardSuccess(CategoryListState state, RegisterCategorySuccessAction action)
        => new() { IsLoading = false, Categories = [..state.Categories, action.Category] };
}