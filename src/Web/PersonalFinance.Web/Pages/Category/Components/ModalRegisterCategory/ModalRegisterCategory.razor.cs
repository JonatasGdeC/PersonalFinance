using FluentValidation.Results;
using PersonalFinance.Adapter.Exceptions;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Enums;
using PersonalFinance.Communication.Requests.Category;
using PersonalFinance.Communication.Validators;
using PersonalFinance.Web.Components.AddInput;
using PersonalFinance.Web.Resources.Transactions;
using PersonalFinance.Web.UseState.Category;
using PersonalFinance.Web.UseState.Modal;

namespace PersonalFinance.Web.Pages.Category.Components.ModalRegisterCategory;

public partial class ModalRegisterCategory
{
    private List<string> _errorMessages = [];
    private bool _isSubmitting;
    private string _categoryName = string.Empty;
    private string _categoryType = ((int)FinancialType.Expense).ToString();
    
    private static readonly List<AddInputOption> TypeOptions =
    [
        new() { Value = ((int)FinancialType.Expense).ToString(), Label = TransactionsResources.Expense },
        new() { Value = ((int)FinancialType.Income).ToString(), Label = TransactionsResources.Income },
    ];

    private async Task HandleRegisterCategory()
    {
        _errorMessages = [];
        
        RegisterCategoryRequest request = new()
        {
            Name = _categoryName,
            Type = Enum.TryParse(value: _categoryType, result: out FinancialType type) ? type : FinancialType.Expense,
        };
        
        CategoryValidator validator = new();
        ValidationResult? result = await validator.ValidateAsync(instance: request);

        if (!result.IsValid)
        {
            _errorMessages = result.Errors.Select(selector: error => error.ErrorMessage).ToList();
            return;
        }
        
        _isSubmitting = true;

        try
        {
            CategoryDto category = await PersonalFinanceApi.Category.Register(request: request);
            Dispatcher.Dispatch(action: new CategoryActions.RegisterCategorySuccessAction(Category: category));
            HandleClose();
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

    private void HandleClose()
    {
        _errorMessages = [];
        Dispatcher.Dispatch(action: new ModalActions.CloseModalAction(Modal: ModalType.AddCategory));
    }
}
