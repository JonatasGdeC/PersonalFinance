using Microsoft.AspNetCore.Components;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Requests;
using PersonalFinance.Communication.Responses.Transaction;

namespace PersonalFinance.Web.Utils.HandlerExpensesByBudget;

public partial class HandlerExpensesByBudget : ComponentBase
{
    [Parameter] public required BudgetDto Budget { get; init; }
    
    private bool _isLoading = true;
    private GetListTransactionsResponse? _transactions;
    
    protected override async Task OnInitializedAsync()
    {
        _transactions = await PersonalFinanceApi.Transaction.GetByCategory(categoryId: Budget.Category.Id, date: DateTime.Now,
            pagination: new PaginationRequest
            {
                PageNumber = 1,
                PageSize = 3
            });
        
        _isLoading = false;
    }
}