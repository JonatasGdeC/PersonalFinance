using System.Globalization;
using Microsoft.AspNetCore.Components;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Requests;
using PersonalFinance.Communication.Responses.Transaction;
using PersonalFinance.Web.Components.AddText;

namespace PersonalFinance.Web.Utils.HandlerExpensesByBudget;

public partial class HandlerExpensesByBudget : ComponentBase, IDisposable
{
    [Parameter] public required BudgetDto Budget { get; init; }
    [Parameter] public TextPreset TextPreset { get; init; } = TextPreset.Preset3;
    [Parameter] public bool ShowRemaining { get; init; }
    [Parameter] public bool ShowList { get; init; }
    [Parameter] public EventCallback<double> OnSpentLoaded { get; set; }

    private bool _isLoading = true;
    private string _showAmount = string.Empty;
    private List<TransactionDto> _transactions = [];

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

        GetListTransactionsResponse? response = await PersonalFinanceApi.Transaction.GetByCategory(categoryId: Budget.Category.Id, date: DateState.Value.CurrentDate,
            pagination: new PaginationRequest
            {
                PageNumber = 1,
                PageSize = 3
            });

        if (response != null)
        {
            double remaining = Budget.MaximumSpend - response.TotalAmount;
            _showAmount = ShowRemaining ? remaining.ToString(format: "C2", provider: CultureInfo.CurrentCulture) : response.TotalAmount.ToString(format: "C2", provider: CultureInfo.CurrentCulture);
            _transactions = response.ListTransactions;

            if (OnSpentLoaded.HasDelegate)
            {
                await OnSpentLoaded.InvokeAsync(arg: response.TotalAmount);
            }
        }

        _isLoading = false;
        StateHasChanged();
    }

    public void Dispose() => DateState.StateChanged -= HandleDateChanged;
}
