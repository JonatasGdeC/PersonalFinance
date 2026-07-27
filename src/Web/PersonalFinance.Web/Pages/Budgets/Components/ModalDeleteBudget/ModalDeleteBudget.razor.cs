using PersonalFinance.Adapter.Exceptions;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Web.Resources.Budgets;
using PersonalFinance.Web.Services.SnackbarService;
using PersonalFinance.Web.UseState.Budget;
using PersonalFinance.Web.UseState.Modal;

namespace PersonalFinance.Web.Pages.Budgets.Components.ModalDeleteBudget;

public partial class ModalDeleteBudget
{
    private List<string> _errorMessages = [];
    private bool _isSubmitting;

    private async Task HandleConfirmDeletion()
    {
        _errorMessages = [];

        BudgetDto? targetBudget = BudgetState.Value.Budget;
        if (targetBudget == null)
        {
            return;
        }

        _isSubmitting = true;

        try
        {
            await PersonalFinanceApi.Budget.Delete(budgetId: targetBudget.Id);
            Dispatcher.Dispatch(action: new BudgetActions.DeleteBudgetSuccessAction(BudgetId: targetBudget.Id));
            HandleClose();
            SnackbarService.Show(message: BudgetsResources.BudgetDeletedSuccessMessage, severity: SnackbarSeverity.Success);
        }
        catch (ApiException exception) when (exception.ErrorMessages.Count > 0)
        {
            _errorMessages = exception.ErrorMessages.ToList();
        }
        catch
        {
            SnackbarService.Show(message: BudgetsResources.UnknownError, severity: SnackbarSeverity.Error);
        }
        finally
        {
            _isSubmitting = false;
        }
    }

    private void HandleClose()
    {
        _errorMessages = [];
        Dispatcher.Dispatch(action: new ModalActions.CloseModalAction(Modal: ModalType.DeleteBudget));
    }
}
