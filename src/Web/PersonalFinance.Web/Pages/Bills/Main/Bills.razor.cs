using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Enums;
using PersonalFinance.Communication.Requests.Bill;
using PersonalFinance.Communication.Responses.Bill;
using PersonalFinance.Web.Components.AddInput;
using PersonalFinance.Web.Resources.Bills;
using PersonalFinance.Web.UseState.Bill;
using PersonalFinance.Web.UseState.Modal;
using PersonalFinance.Web.Utils.HandlerBillDueDate;

namespace PersonalFinance.Web.Pages.Bills.Main;

public partial class Bills
{
    private static readonly List<AddInputOption> SortOptions =
    [
        new() { Value = ((int)ListOrder.Latest).ToString(), Label = BillsResources.Latest },
        new() { Value = ((int)ListOrder.Oldest).ToString(), Label = BillsResources.Oldest },
        new() { Value = ((int)ListOrder.Az).ToString(), Label = BillsResources.AToZ },
        new() { Value = ((int)ListOrder.Za).ToString(), Label = BillsResources.ZToA },
        new() { Value = ((int)ListOrder.Highest).ToString(), Label = BillsResources.Highest },
        new() { Value = ((int)ListOrder.Lowest).ToString(), Label = BillsResources.Lowest },
    ];

    private bool _isLoading = true;
    private string? _search;
    private ListOrder _listOrder = ListOrder.Latest;

    private string SortValue => ((int)_listOrder).ToString();

    private List<BillDto> FilteredBills
    {
        get
        {
            IEnumerable<BillDto> bills = BillListState.Value.Bills;

            if (!string.IsNullOrWhiteSpace(value: _search))
            {
                string search = _search.ToLower();
                bills = bills.Where(predicate: bill =>
                    bill.Participant.Name.ToLower().Contains(search) ||
                    (bill.Category != null && bill.Category.Name.ToLower().Contains(search)));
            }

            bills = _listOrder switch
            {
                ListOrder.Oldest => bills.OrderBy(keySelector: bill => bill.DueDate),
                ListOrder.Az => bills.OrderBy(keySelector: bill => bill.Participant.Name),
                ListOrder.Za => bills.OrderByDescending(keySelector: bill => bill.Participant.Name),
                ListOrder.Highest => bills.OrderByDescending(keySelector: bill => bill.Amount),
                ListOrder.Lowest => bills.OrderBy(keySelector: bill => bill.Amount),
                _ => bills.OrderByDescending(keySelector: bill => bill.DueDate)
            };

            return bills.ToList();
        }
    }

    private double TotalBillsAmount => BillListState.Value.Bills.Sum(selector: bill => bill.Amount);

    private (int Count, double Amount) PaidSummary => Summarize(bills: BillListState.Value.Bills.Where(predicate: BillStatusHelper.IsPaid));

    private (int Count, double Amount) UpcomingSummary => Summarize(bills: BillListState.Value.Bills.Where(predicate: bill => !BillStatusHelper.IsPaid(bill: bill)));

    private (int Count, double Amount) DueSoonSummary => Summarize(bills: BillListState.Value.Bills.Where(predicate: BillStatusHelper.IsDueSoon));

    private static (int Count, double Amount) Summarize(IEnumerable<BillDto> bills)
    {
        List<BillDto> list = bills.ToList();
        return (list.Count, list.Sum(selector: bill => bill.Amount));
    }

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

    private void OpenAddBillModal() => Dispatcher.Dispatch(action: new ModalActions.OpenModalAction(Modal: ModalType.AddBill));

    private void HandleSearchChanged(string? value) => _search = value;

    private void HandleSortChanged(string? value)
    {
        _listOrder = Enum.TryParse(value: value, result: out ListOrder listOrder) ? listOrder : ListOrder.Latest;
    }
}
