using PersonalFinance.Communication.Responses.Transaction;

namespace PersonalFinance.Web.Pages.Home.Main;

public partial class Home
{
    private bool _isLoading = true;
    private GetTransactionDashboardResponse? _transactionDashboard;

    protected override async Task OnInitializedAsync()
    {
        _transactionDashboard = await PersonalFinanceApi.Transaction.GetDashboard(date: DateTime.Today);
        _isLoading = false;
    }
}