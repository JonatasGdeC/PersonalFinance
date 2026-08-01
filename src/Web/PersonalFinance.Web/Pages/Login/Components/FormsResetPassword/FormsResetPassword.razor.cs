using FluentValidation.Results;
using Microsoft.AspNetCore.Components;
using PersonalFinance.Adapter.Exceptions;
using PersonalFinance.Communication.Requests.User;
using PersonalFinance.Communication.Responses.User;
using PersonalFinance.Communication.Validators;
using PersonalFinance.Web.Resources.Login;
using PersonalFinance.Web.Services.SnackbarService;

namespace PersonalFinance.Web.Pages.Login.Components.FormsResetPassword;

public partial class FormsResetPassword : ComponentBase
{
    [Parameter] public EventCallback NavigateToLogin { get; set; }

    private string _email = string.Empty;
    private string _code = string.Empty;
    private string _tokenResetPassword = string.Empty;
    private string _newPassword = string.Empty;
    private string _confirmPassword = string.Empty;

    private List<string> _errorMessage = [];
    private bool _isSubmitting;

    private ResetStep _step = ResetStep.Email;
    
    private async Task HandleForgotPassword()
    {
        _errorMessage = [];

        if (string.IsNullOrWhiteSpace(value: _email))
        {
            _errorMessage = [LoginResources.EmailRequiredError];
            return;
        }

        _isSubmitting = true;

        try
        {
            await PersonalFinanceApi.User.ForgotPassword(request: new ForgotPasswordRequest
            {
                Email = _email
            });

            _step = ResetStep.Code;
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

    private async Task HandleValidateResetCode()
    {
        _errorMessage = [];

        if (string.IsNullOrWhiteSpace(value: _code))
        {
            _errorMessage = [LoginResources.CodeRequiredError];
            return;
        }

        _isSubmitting = true;

        try
        {
            ValidateResetCodeResponse response = await PersonalFinanceApi.User.ValidateResetCode(
                request: new ValidateResetCodeRequest
                {
                    Email = _email,
                    Code = _code
                });

            _tokenResetPassword = response.TokenResetPassword;
            _step = ResetStep.NewPassword;
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

    private async Task HandleResetPassword()
    {
        _errorMessage = [];

        if (_newPassword != _confirmPassword)
        {
            _errorMessage = [LoginResources.PasswordsDoNotMatchError];
            return;
        }

        ValidationResult resultPassword = await new PasswordValidator().ValidateAsync(instance: _newPassword);
        if (!resultPassword.IsValid)
        {
            _errorMessage = resultPassword.Errors.Select(selector: error => error.ErrorMessage).ToList();
            return;
        }

        _isSubmitting = true;

        try
        {
            await PersonalFinanceApi.User.ResetPassword(request: new ResetPasswordRequest
            {
                NewPassword = _newPassword,
                TokenResetPassword = _tokenResetPassword
            });

            SnackbarService.Show(message: LoginResources.PasswordChangedSuccess, severity: SnackbarSeverity.Success);
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

enum ResetStep
{
    Email,
    Code,
    NewPassword
}