using Fluxor;
using PersonalFinance.Web.UseState.User.State;

namespace PersonalFinance.Web.UseState.User.Reducers;

using static UserActions;

public class ReducerGetCurrentUser
{
    [ReducerMethod(actionType: typeof(GetCurrentUserAction))]
    public static UserState ReduceGetCurrentUser(UserState state)
        => new() { IsLoading = true, User = state.User };

    [ReducerMethod]
    public static UserState ReduceGetCurrentUserSuccess(UserState state, GetCurrentUserSuccessAction action)
        => new() { IsLoading = false, User = action.User };
}
