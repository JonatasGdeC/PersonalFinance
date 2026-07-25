using PersonalFinance.Communication.Responses.Transaction;

namespace PersonalFinance.Web.Pages.Home.Main;

public partial class Home : IDisposable
{
    private bool _isLoading = true;
    private GetTransactionDashboardResponse? _transactionDashboard;

    protected override async Task OnInitializedAsync()
    {
        DateState.StateChanged += HandleDateChanged;
        await LoadDashboard();
    }

    private async void HandleDateChanged(object? sender, EventArgs e) => await InvokeAsync(workItem: LoadDashboard);

    private async Task LoadDashboard()
    {
        _isLoading = true;
        StateHasChanged();

        _transactionDashboard = await PersonalFinanceApi.Transaction.GetDashboard(date: DateState.Value.CurrentDate);

        _isLoading = false;
        StateHasChanged();
    }

    public void Dispose() => DateState.StateChanged -= HandleDateChanged;
}