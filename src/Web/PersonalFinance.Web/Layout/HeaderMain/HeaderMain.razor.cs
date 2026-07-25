using PersonalFinance.Web.UseState.Date;

namespace PersonalFinance.Web.Layout.HeaderMain;

public partial class HeaderMain
{
    private void NavigateToLastDate() =>
        Dispatcher.Dispatch(action: new DateActions.ChangeDateAction(Date: DateState.Value.CurrentDate.AddMonths(months: -1)));

    private void NavigateToNextDate() =>
        Dispatcher.Dispatch(action: new DateActions.ChangeDateAction(Date: DateState.Value.CurrentDate.AddMonths(months: 1)));
}