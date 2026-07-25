using Microsoft.AspNetCore.Components;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Requests;
using PersonalFinance.Communication.Responses.Transaction;

namespace PersonalFinance.Web.Utils.HandlerExpensesByBudget;

public partial class HandlerExpensesByBudget : ComponentBase, IDisposable
{
    [Parameter] public required BudgetDto Budget { get; init; }

    private bool _isLoading = true;
    private GetListTransactionsResponse? _transactions;

    protected override async Task OnInitializedAsync()
    {
        DateState.StateChanged += HandleDateChanged;
        await LoadTransactions();
    }

    private async void HandleDateChanged(object? sender, EventArgs e) => await InvokeAsync(workItem: LoadTransactions);

    private async Task LoadTransactions()
    {
        _isLoading = true;
        StateHasChanged();

        _transactions = await PersonalFinanceApi.Transaction.GetByCategory(categoryId: Budget.Category.Id, date: DateState.Value.CurrentDate,
            pagination: new PaginationRequest
            {
                PageNumber = 1,
                PageSize = 3
            });

        _isLoading = false;
        StateHasChanged();
    }

    public void Dispose() => DateState.StateChanged -= HandleDateChanged;
}