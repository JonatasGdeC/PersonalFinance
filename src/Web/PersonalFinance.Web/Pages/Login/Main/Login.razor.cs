namespace PersonalFinance.Web.Pages.Login.Main;

public partial class Login
{
    private bool _showPageLogin = true;
    private bool _showPageRegister;
    private bool _showPageForgotPassword;

    private void ShowPageLogin()
    {
        _showPageLogin = true;
        _showPageRegister = false;
        _showPageForgotPassword = false;
    }

    private void ShowPageRegister()
    {
        _showPageRegister = true;
        _showPageLogin = false;
        _showPageForgotPassword = false;
    }

    private void ShowPageForgotPassword()
    {
        _showPageForgotPassword = true;
        _showPageLogin = false;
        _showPageRegister = false;
    }
}
