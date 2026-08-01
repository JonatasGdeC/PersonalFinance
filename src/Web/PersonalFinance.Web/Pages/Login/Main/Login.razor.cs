namespace PersonalFinance.Web.Pages.Login.Main;

public partial class Login
{
    private bool _showPageLogin = true;
    private bool _showPageRegister;
    private bool _showPageResetPassword;

    private void ShowPageLogin()
    {
        _showPageLogin = true;
        _showPageRegister = false;
        _showPageResetPassword = false;
    }

    private void ShowPageRegister()
    {
        _showPageRegister = true;
        _showPageLogin = false;
        _showPageResetPassword = false;
    }

    private void ShowPageForgotPassword()
    {
        _showPageResetPassword = true;
        _showPageLogin = false;
        _showPageRegister = false;
    }
    
}
