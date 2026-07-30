using Microsoft.AspNetCore.Components;
using PersonalFinance.Adapter.Exceptions;
using PersonalFinance.Communication.Requests.User;
using PersonalFinance.Web.Resources.Login;
using PersonalFinance.Web.Services.SnackbarService;

namespace PersonalFinance.Web.Pages.Login.Components.FormForgotPassword;

public partial class FormForgotPassword : ComponentBase
{
    [Parameter] public EventCallback NavigateToLogin { get; set; }

    private readonly ForgotPasswordRequest _forgotPasswordRequest = new()
    {
        Email = string.Empty
    };

    private List<string> _errorMessage = [];
    private bool _isSubmitting;

    private async Task HandleForgotPassword()
    {
        _errorMessage = [];

        if (string.IsNullOrWhiteSpace(value: _forgotPasswordRequest.Email))
        {
            _errorMessage = [LoginResources.EmailRequiredError];
            return;
        }

        _isSubmitting = true;

        try
        {
            await PersonalFinanceApi.User.ForgotPassword(request: _forgotPasswordRequest);
            SnackbarService.Show(message: LoginResources.CodeSentSuccess, severity: SnackbarSeverity.Success);
            await NavigateToLogin.InvokeAsync();
        }
        catch (ApiException exception) when (exception.ErrorMessages.Count > 0)
        {
            _errorMessage = exception.ErrorMessages.ToList();
        }
        catch
        {
            SnackbarService.Show(message: LoginResources.UnknownError, severity: SnackbarSeverity.Error);
        }
        finally
        {
            _isSubmitting = false;
        }
    }
}
