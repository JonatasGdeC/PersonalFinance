using FluentValidation.Results;
using Microsoft.AspNetCore.Components;
using PersonalFinance.Adapter.Exceptions;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Requests.User;
using PersonalFinance.Communication.Validators;
using PersonalFinance.Web.Resources.Profile;
using PersonalFinance.Web.Services.SnackbarService;
using PersonalFinance.Web.UseState.Modal;
using PersonalFinance.Web.UseState.User;

namespace PersonalFinance.Web.Pages.Profile.Main;

public partial class Profile
{
    private string _name = string.Empty;
    private string _email = string.Empty;
    private string? _profileImage;
    private bool _receiveEmailNotifications = true;

    private List<string> _profileErrorMessages = [];
    private bool _isSubmittingProfile;

    private string _oldPassword = string.Empty;
    private string _newPassword = string.Empty;

    private List<string> _passwordErrorMessages = [];
    private bool _isSubmittingPassword;

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

        SeedForm();

        await base.OnInitializedAsync();
    }

    private void SeedForm()
    {
        UserDto? user = UserState.Value.User;
        if (user == null)
        {
            return;
        }

        _name = user.Name;
        _email = user.Email;
        _profileImage = user.ProfileImage;
        _receiveEmailNotifications = user.EmailNotificationsEnabled;
    }

    private void HandleEmailNotificationsChanged(ChangeEventArgs e) => _receiveEmailNotifications = e.Value is bool value && value;

    private async Task HandleUpdateProfile()
    {
        _profileErrorMessages = [];

        UpdateUserRequest request = new()
        {
            Name = _name,
            Email = _email,
            ProfileImage = string.IsNullOrWhiteSpace(value: _profileImage) ? null : _profileImage,
            EmailNotificationsEnabled = _receiveEmailNotifications
        };

        UpdateUserValidator validator = new();
        ValidationResult? result = await validator.ValidateAsync(instance: request);

        if (!result.IsValid)
        {
            _profileErrorMessages = result.Errors.Select(selector: error => error.ErrorMessage).ToList();
            return;
        }

        _isSubmittingProfile = true;

        try
        {
            await PersonalFinanceApi.User.Update(request: request);

            UserDto updatedUser = new()
            {
                Name = request.Name,
                Email = request.Email,
                ProfileImage = request.ProfileImage,
                EmailNotificationsEnabled = request.EmailNotificationsEnabled
            };

            Dispatcher.Dispatch(action: new UserActions.UpdateCurrentUserSuccessAction(User: updatedUser));
            SnackbarService.Show(message: ProfileResources.ProfileUpdatedSuccessMessage, severity: SnackbarSeverity.Success);
        }
        catch (ApiException exception) when (exception.ErrorMessages.Count > 0)
        {
            _profileErrorMessages = exception.ErrorMessages.ToList();
        }
        catch
        {
            SnackbarService.Show(message: ProfileResources.UnknownError, severity: SnackbarSeverity.Error);
        }
        finally
        {
            _isSubmittingProfile = false;
        }
    }

    private async Task HandleChangePassword()
    {
        _passwordErrorMessages = [];

        if (string.IsNullOrWhiteSpace(value: _oldPassword) || string.IsNullOrWhiteSpace(value: _newPassword))
        {
            _passwordErrorMessages = [ProfileResources.RequiredFieldsError];
            return;
        }

        UpdatePasswordRequest request = new()
        {
            OldPassword = _oldPassword,
            NewPassword = _newPassword
        };

        _isSubmittingPassword = true;

        try
        {
            await PersonalFinanceApi.User.UpdatePassword(request: request);
            _oldPassword = string.Empty;
            _newPassword = string.Empty;
            SnackbarService.Show(message: ProfileResources.PasswordUpdatedSuccessMessage, severity: SnackbarSeverity.Success);
        }
        catch (ApiException exception) when (exception.ErrorMessages.Count > 0)
        {
            _passwordErrorMessages = exception.ErrorMessages.ToList();
        }
        catch
        {
            SnackbarService.Show(message: ProfileResources.UnknownError, severity: SnackbarSeverity.Error);
        }
        finally
        {
            _isSubmittingPassword = false;
        }
    }

    private void OpenDeleteAccountModal() => Dispatcher.Dispatch(action: new ModalActions.OpenModalAction(Modal: ModalType.DeleteAccount));
}
