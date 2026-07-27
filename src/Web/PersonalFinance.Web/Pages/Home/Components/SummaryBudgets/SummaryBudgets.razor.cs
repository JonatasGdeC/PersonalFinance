using PersonalFinance.Communication.Responses.Budget;
using PersonalFinance.Web.UseState.Budget;

namespace PersonalFinance.Web.Pages.Home.Components.SummaryBudgets;

public partial class SummaryBudgets
{
    private bool _isLoading = true;

    private double TotalLimit => BudgetListState.Value.Budgets.Sum(selector: budget => budget.MaximumSpend);

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

    private void NavigateToBudgets() => NavigationManager.NavigateTo(uri: "/budgets");
}
