using PersonalFinance.Adapter.Exceptions;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Web.Resources.Pots;
using PersonalFinance.Web.Services.SnackbarService;
using PersonalFinance.Web.UseState.Modal;
using PersonalFinance.Web.UseState.Pot;

namespace PersonalFinance.Web.Pages.Pots.Components.ModalDeletePot;

public partial class ModalDeletePot
{
    private List<string> _errorMessages = [];
    private bool _isSubmitting;

    private async Task HandleConfirmDeletion()
    {
        _errorMessages = [];

        PotDto? targetPot = PotState.Value.Pot;
        if (targetPot == null)
        {
            return;
        }

        _isSubmitting = true;

        try
        {
            await PersonalFinanceApi.Pot.Delete(potId: targetPot.Id);
            Dispatcher.Dispatch(action: new PotActions.DeletePotSuccessAction(PotId: targetPot.Id));
            HandleClose();
            SnackbarService.Show(message: PotsResources.PotDeletedSuccessMessage, severity: SnackbarSeverity.Success);
        }
        catch (ApiException exception) when (exception.ErrorMessages.Count > 0)
        {
            _errorMessages = exception.ErrorMessages.ToList();
        }
        catch
        {
            SnackbarService.Show(message: PotsResources.UnknownError, severity: SnackbarSeverity.Error);
        }
        finally
        {
            _isSubmitting = false;
        }
    }

    private void HandleClose()
    {
        _errorMessages = [];
        Dispatcher.Dispatch(action: new ModalActions.CloseModalAction(Modal: ModalType.DeletePot));
    }
}
