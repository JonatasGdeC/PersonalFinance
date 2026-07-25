using PersonalFinance.Communication.Responses.Budget;
using PersonalFinance.Web.UseState.Budget;
using PersonalFinance.Web.UseState.Modal;

namespace PersonalFinance.Web.Pages.Budgets.Main;

public partial class Budgets
{
    private bool _isLoading = true;

    protected override async Task OnInitializedAsync()
    {
        if (!BudgetListState.Value.Budgets.Any())
        {
            Dispatcher.Dispatch(action: new BudgetActions.GetAllBudgetsAction());
            GetAllBudgetResponse? response = await PersonalFinanceApi.Budget.GetAll();
            if (response != null)
            {
                Dispatcher.Dispatch(action: new BudgetActions.GetAllBudgetsSuccessAction(Budgets: response.ListBudgets));
            }
        }

        _isLoading = false;

        await base.OnInitializedAsync();
    }

    private void OpenAddBudgetModal() => Dispatcher.Dispatch(action: new ModalActions.OpenModalAction(Modal: ModalType.AddBudget));
}
