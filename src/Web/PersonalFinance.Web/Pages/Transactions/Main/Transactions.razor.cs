using Microsoft.AspNetCore.Components;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Enums;
using PersonalFinance.Communication.Requests.Transaction;
using PersonalFinance.Communication.Responses.Category;
using PersonalFinance.Communication.Responses.Transaction;
using PersonalFinance.Web.Components.AddInput;
using PersonalFinance.Web.Resources.Transactions;

namespace PersonalFinance.Web.Pages.Transactions.Main;

public partial class Transactions : ComponentBase
{
    private const string AllCategoriesValue = "";

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
    private List<AddInputOption> _categoryOptions = [];
    private List<AddInputOption> _categoryFormOptions = [];
    private bool _isAddTransactionModalOpen;

    private string SortValue => ((int)_filterRequest.ListOrder).ToString();
    private string CategoryValue => _filterRequest.CategoryId?.ToString() ?? AllCategoriesValue;

    protected override async Task OnInitializedAsync()
    {
        GetAllCategoryResponse? categories = await PersonalFinanceApi.Category.GetAll();

        _categoryFormOptions = (categories?.ListCategories ?? [])
            .Select(selector: category => new AddInputOption { Value = category.Id.ToString(), Label = category.Name })
            .ToList();

        _categoryOptions =
        [
            new AddInputOption { Value = AllCategoriesValue, Label = TransactionsResources.AllTransactions },
            .. _categoryFormOptions
        ];

        await LoadTransactions();
    }

    private void OpenAddTransactionModal() => _isAddTransactionModalOpen = true;

    private void CloseAddTransactionModal() => _isAddTransactionModalOpen = false;

    private async Task HandleTransactionRegistered()
    {
        _isAddTransactionModalOpen = false;
        await LoadTransactions();
    }

    private void HandleCategoryRegistered(CategoryDto category)
    {
        AddInputOption option = new() { Value = category.Id.ToString(), Label = category.Name };
        _categoryFormOptions.Add(item: option);
        _categoryOptions.Add(item: option);
    }

    private async Task LoadTransactions()
    {
        _isLoading = true;
        _transactions = await PersonalFinanceApi.Transaction.GetAll(request: _filterRequest);
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
        _filterRequest.CategoryId = long.TryParse(s: value, result: out long categoryId) ? categoryId : null;
        _filterRequest.Pagination.PageNumber = 1;
        await LoadTransactions();
    }

    private async Task HandlePageChanged(int page)
    {
        _filterRequest.Pagination.PageNumber = page;
        await LoadTransactions();
    }
}