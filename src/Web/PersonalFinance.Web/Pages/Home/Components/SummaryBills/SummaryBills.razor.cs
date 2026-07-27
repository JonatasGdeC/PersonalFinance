using PersonalFinance.Communication.Requests.Bill;
using PersonalFinance.Communication.Responses.Bill;
using PersonalFinance.Web.UseState.Bill;
using PersonalFinance.Web.Utils.HandlerBillDueDate;

namespace PersonalFinance.Web.Pages.Home.Components.SummaryBills;

public partial class SummaryBills
{
    private bool _isLoading = true;

    private double PaidAmount => BillStatusHelper.Summarize(bills: BillListState.Value.Bills.Where(predicate: BillStatusHelper.IsPaid)).Amount;

    private double UpcomingAmount => BillStatusHelper.Summarize(bills: BillListState.Value.Bills.Where(predicate: bill => !BillStatusHelper.IsPaid(bill: bill))).Amount;

    private double DueSoonAmount => BillStatusHelper.Summarize(bills: BillListState.Value.Bills.Where(predicate: BillStatusHelper.IsDueSoon)).Amount;

    protected override async Task OnInitializedAsync()
    {
        if (!BillListState.Value.Bills.Any())
        {
            Dispatcher.Dispatch(action: new BillActions.GetAllBillsAction());

            GetAllBillResponse? response = await PersonalFinanceApi.Bill.GetAll(filter: new BillFilterRequest
            {
                Pagination = { PageSize = 100 }
            });

            if (response != null)
            {
                Dispatcher.Dispatch(action: new BillActions.GetAllBillsSuccessAction(Bills: response.ListBills));
            }
        }

        _isLoading = false;

        await base.OnInitializedAsync();
    }

    private void NavigateToBills() => NavigationManager.NavigateTo(uri: "/recurring-bills");
}
