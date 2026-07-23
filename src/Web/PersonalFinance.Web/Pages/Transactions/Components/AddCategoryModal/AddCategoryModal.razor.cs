using Microsoft.AspNetCore.Components;
using PersonalFinance.Adapter.Exceptions;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Enums;
using PersonalFinance.Communication.Requests.Category;
using PersonalFinance.Web.Resources.Transactions;

namespace PersonalFinance.Web.Pages.Transactions.Components.AddCategoryModal;

public partial class AddCategoryModal : ComponentBase
{
    [Parameter] public bool IsOpen { get; set; }
    [Parameter] public EventCallback OnClose { get; set; }
    [Parameter] public EventCallback<CategoryDto> OnRegistered { get; set; }
    [Parameter] public required string CategoryType { get; set; }

    private string? _name;
    private List<string> _errorMessages = [];
    private bool _isSubmitting;

    private async Task HandleSubmit()
    {
        _errorMessages = [];

        if (string.IsNullOrWhiteSpace(value: _name))
        {
            _errorMessages = [TransactionsResources.RequiredFieldsError];
            return;
        }

        _isSubmitting = true;

        RegisterCategoryRequest request = new()
        {
            Name = _name,
            Type = Enum.TryParse(value: CategoryType, result: out TransactionType type) ? type : TransactionType.Expense
        };

        try
        {
            CategoryDto category = await PersonalFinanceApi.Category.Register(request: request);

            _name = null;

            if (OnRegistered.HasDelegate)
            {
                await OnRegistered.InvokeAsync(arg: category);
            }
        }
        catch (ApiException exception) when (exception.ErrorMessages.Count > 0)
        {
            _errorMessages = exception.ErrorMessages.ToList();
        }
        catch
        {
            _errorMessages = [TransactionsResources.UnknownError];
        }
        finally
        {
            _isSubmitting = false;
        }
    }

    private async Task HandleClose()
    {
        _errorMessages = [];

        if (OnClose.HasDelegate)
        {
            await OnClose.InvokeAsync();
        }
    }
}
