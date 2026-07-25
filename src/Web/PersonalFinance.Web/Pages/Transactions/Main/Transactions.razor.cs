using Microsoft.AspNetCore.Components;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Enums;
using PersonalFinance.Communication.Requests.Transaction;
using PersonalFinance.Communication.Responses.Category;
using PersonalFinance.Communication.Responses.Transaction;
using PersonalFinance.Web.Components.AddInput;
using PersonalFinance.Web.Resources.Transactions;
using PersonalFinance.Web.UseState.Category;
using PersonalFinance.Web.UseState.Modal;
using PersonalFinance.Web.UseState.Transaction;

namespace PersonalFinance.Web.Pages.Transactions.Main;

public partial class Transactions : ComponentBase
{
    private static readonly List<AddInputOption> SortOptions =
    [
        new() { Value = ((int)ListOrder.Latest).ToString(), Label = TransactionsResources.Latest },
        new() { Value = ((int)ListOrder.Oldest).ToString(), Label = TransactionsResources.Oldest },
        new() { Value = ((int)ListOrder.Az).ToString(), Label = TransactionsResources.AToZ },
        new() { Value = ((int)ListOrder.Za).ToString(), Label = TransactionsResources.ZToA },
        new() { Value = ((int)ListOrder.Highest).ToString(), Label = TransactionsResources.Highest },
        new() { Value = ((int)ListOrder.Lowest).ToString(), Label = TransactionsResources.Lowest },
    ];

    private bool _isLoading = true;
    private readonly TransactionFilterRequest _filterRequest = new();
    private GetListTransactionsResponse? _transactions;
    
    private List<AddInputOption> CategoryOptions => CategoryListState.Value.Categories
        .Select(selector: category => new AddInputOption { Value = category.Id.ToString(), Label = category.Name })
        .ToList();


    private string SortValue => ((int)_filterRequest.ListOrder).ToString();
    private string CategoryValue => _filterRequest.CategoryId?.ToString() ?? string.Empty;

    protected override async Task OnInitializedAsync()
    {
        Dispatcher.Dispatch(action: new CategoryActions.GetAllCategoriesAction());

        try
        {
            if (!CategoryListState.Value.Categories.Any())
            {
                Dispatcher.Dispatch(action: new CategoryActions.GetAllCategoriesAction());
                GetAllCategoryResponse? response = await PersonalFinanceApi.Category.GetAll();
                if (response != null)
                {
                    Dispatcher.Dispatch(action: new CategoryActions.GetAllCategoriesSuccessAction(Categories: response.ListCategories));
                }
            }
            
            await LoadTransactions();
            
            _isLoading = false;
        }
        catch { /* ignored */ }
    }

    private void OpenAddTransactionModal() => Dispatcher.Dispatch(action: new ModalActions.OpenModalAction(Modal: ModalType.AddTransaction));

    private async Task LoadTransactions()
    {
        _isLoading = true;
        _transactions = await PersonalFinanceApi.Transaction.GetAll(request: _filterRequest);
        if (_transactions != null)
        {
            Dispatcher.Dispatch(action: new TransactionActions.GetAllTransactionsSuccessAction(Transactions: _transactions.ListTransactions));
        }
        _isLoading = false;
    }

    private async Task HandleSearchChanged(string? value)
    {
        await Task.Delay(millisecondsDelay: 300);
        _filterRequest.Search = value;
        _filterRequest.Pagination.PageNumber = 1;
        await LoadTransactions();
    }

    private async Task HandleSortChanged(string? value)
    {
        _filterRequest.ListOrder = Enum.TryParse(value: value, result: out ListOrder listOrder) ? listOrder : ListOrder.Latest;
        await LoadTransactions();
    }

    private async Task HandleCategoryChanged(string? value)
    {
        _filterRequest.CategoryId = Guid.TryParse(input: value, result: out Guid categoryId) ? categoryId : null;
        _filterRequest.Pagination.PageNumber = 1;
        await LoadTransactions();
    }

    private async Task HandlePageChanged(int page)
    {
        _filterRequest.Pagination.PageNumber = page;
        await LoadTransactions();
    }
}