using PersonalFinance.Adapter.Exceptions;
using PersonalFinance.Web.Resources.Profile;
using PersonalFinance.Web.Services.SnackbarService;
using PersonalFinance.Web.UseState.Modal;

namespace PersonalFinance.Web.Pages.Profile.Components.ModalDeleteAccount;

public partial class ModalDeleteAccount
{
    private List<string> _errorMessages = [];
    private bool _isSubmitting;

    private async Task HandleConfirmDeletion()
    {
        _errorMessages = [];
        _isSubmitting = true;

        try
        {
            await PersonalFinanceApi.User.Delete();
            HandleClose();
            SnackbarService.Show(message: ProfileResources.AccountDeletedSuccessMessage, severity: SnackbarSeverity.Success);
            await AuthenticationStateProvider.RemoveTokenAsync();
            NavigationManager.NavigateTo(uri: "/", forceLoad: true);
        }
        catch (ApiException exception) when (exception.ErrorMessages.Count > 0)
        {
            _errorMessages = exception.ErrorMessages.ToList();
        }
        catch
        {
            SnackbarService.Show(message: ProfileResources.UnknownError, severity: SnackbarSeverity.Error);
        }
        finally
        {
            _isSubmitting = false;
        }
    }

    private void HandleClose()
    {
        _errorMessages = [];
        Dispatcher.Dispatch(action: new ModalActions.CloseModalAction(Modal: ModalType.DeleteAccount));
    }
}
