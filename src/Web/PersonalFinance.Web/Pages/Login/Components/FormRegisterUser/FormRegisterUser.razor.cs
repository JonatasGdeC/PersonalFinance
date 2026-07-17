using Microsoft.AspNetCore.Components;
using PersonalFinance.Adapter.Exceptions;
using PersonalFinance.Communication.Requests.User;
using PersonalFinance.Communication.Responses.User;

namespace PersonalFinance.Web.Pages.Login.Components.FormRegisterUser;

public partial class FormRegisterUser : ComponentBase
{
    [Parameter] public EventCallback NavigateToLogin { get; set; }
    
    private readonly RegisterUserRequest _registerUserRequest = new()
    {
        Name = string.Empty,
        Email = string.Empty,
        Password = string.Empty,
    };
    
    private List<string> _errorMessage = [];
    private bool _isSubmitting;

    private async Task HandleRegisterUser()
    {
        _errorMessage = [];

        if (string.IsNullOrWhiteSpace(value: _registerUserRequest.Name) || string.IsNullOrWhiteSpace(value: _registerUserRequest.Email) || string.IsNullOrWhiteSpace(value: _registerUserRequest.Password))
        {
            _errorMessage = ["All fields are required."];
            return;
        }

        _isSubmitting = true;

        try
        {
            RegisterUserResponse response = await PersonalFinanceApi.User.Register(request: _registerUserRequest);
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