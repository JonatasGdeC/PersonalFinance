using PersonalFinance.Communication.Dtos;
using PersonalFinance.Web.UseState.Date;
using PersonalFinance.Web.UseState.User;

namespace PersonalFinance.Web.Layout.HeaderMain;

public partial class HeaderMain
{
    private bool _isUserMenuOpen;

    protected override async Task OnInitializedAsync()
    {
        if (UserState.Value.User == null)
        {
            Dispatcher.Dispatch(action: new UserActions.GetCurrentUserAction());
            UserDto? user = await PersonalFinanceApi.User.Get();
            if (user != null)
            {
                Dispatcher.Dispatch(action: new UserActions.GetCurrentUserSuccessAction(User: user));
            }
        }

        await base.OnInitializedAsync();
    }

    private void NavigateToLastDate() =>
        Dispatcher.Dispatch(action: new DateActions.ChangeDateAction(Date: DateState.Value.CurrentDate.AddMonths(months: -1)));

    private void NavigateToNextDate() =>
        Dispatcher.Dispatch(action: new DateActions.ChangeDateAction(Date: DateState.Value.CurrentDate.AddMonths(months: 1)));

    private static string GetInitial(string name) => name.Length > 0 ? name[..1].ToUpperInvariant() : string.Empty;

    private void ToggleUserMenu() => _isUserMenuOpen = !_isUserMenuOpen;

    private void CloseUserMenu() => _isUserMenuOpen = false;

    private void HandleEditProfileClick()
    {
        _isUserMenuOpen = false;
        NavigationManager.NavigateTo(uri: "/profile");
    }

    private async Task HandleLogoutClick()
    {
        _isUserMenuOpen = false;
        await AuthenticationStateProvider.RemoveTokenAsync();
        NavigationManager.NavigateTo(uri: "/");
    }
}
