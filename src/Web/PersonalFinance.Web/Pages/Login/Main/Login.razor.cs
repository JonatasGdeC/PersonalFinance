namespace PersonalFinance.Web.Pages.Login.Main;

public partial class Login
{
    private bool _showPageLogin = true;
    private bool _showPageRegister;

    private void ShowPageLogin()
    {
        _showPageLogin = true;
        _showPageRegister = false;
    }

    private void ShowPageRegister()
    {
        _showPageRegister = true;
        _showPageLogin = false;
    }
}
