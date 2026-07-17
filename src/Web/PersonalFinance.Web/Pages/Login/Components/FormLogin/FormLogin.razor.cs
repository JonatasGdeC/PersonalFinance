using Microsoft.AspNetCore.Components;
using PersonalFinance.Adapter.Exceptions;
using PersonalFinance.Communication.Requests.User;
using PersonalFinance.Communication.Responses.User;

namespace PersonalFinance.Web.Pages.Login.Components.FormLogin;

public partial class FormLogin : ComponentBase
{
    private readonly LoginRequest _loginRequest = new()
    {
        Email = string.Empty,
        Password = string.Empty
    };

    private List<string> _errorMessage = [];
    private bool _isSubmitting;

    private async Task HandleLogin()
    {
        _errorMessage = [];

        if (string.IsNullOrWhiteSpace(value: _loginRequest.Email) || string.IsNullOrWhiteSpace(value: _loginRequest.Password))
        {
            _errorMessage = ["Email and password are required."];
            return;
        }

        _isSubmitting = true;

        try
        {
            LoginResponse response = await PersonalFinanceApi.User.Login(request: _loginRequest);
            await AuthenticationStateProvider.SetTokenAsync(token: response.Token);
            NavigationManager.NavigateTo(uri: "/");
        }
        catch (ApiException exception) when (exception.ErrorMessages.Count > 0)
        {
            _errorMessage = exception.ErrorMessages.ToList();
        }
        catch
        {
            _errorMessage = ["An error occured while logging in."];
        }
        finally
        {
            _isSubmitting = false;
        }
    }
}