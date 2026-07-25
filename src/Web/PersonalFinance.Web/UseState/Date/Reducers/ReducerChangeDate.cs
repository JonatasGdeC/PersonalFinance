using Fluxor;
using PersonalFinance.Web.UseState.Date.State;

namespace PersonalFinance.Web.UseState.Date.Reducers;

using static DateActions;

public class ReducerChangeDate
{
    [ReducerMethod]
    public static DateState ReduceChangeDate(DateState state, ChangeDateAction action)
        => new() { CurrentDate = action.Date };
}
