using Microsoft.AspNetCore.Components;
using PersonalFinance.Communication.Responses.Budget;

namespace PersonalFinance.Web.Pages.Budgets.Main;

public partial class Budgets : ComponentBase
{
    private bool _isLoading = true;
    private GetAllBudgetResponse? _allBudgetResponse;

    protected override async Task OnInitializedAsync()
    {
        _allBudgetResponse = await PersonalFinanceApi.Budget.GetAll();
        _isLoading = false;
    }
}