using Fluxor;
using PersonalFinance.Web.UseState.User.State;

namespace PersonalFinance.Web.UseState.User.Reducers;

using static UserActions;

public class ReducerUpdateCurrentUser
{
    [ReducerMethod]
    public static UserState ReduceUpdateCurrentUserSuccess(UserState state, UpdateCurrentUserSuccessAction action)
        => new() { IsLoading = false, User = action.User };
}
