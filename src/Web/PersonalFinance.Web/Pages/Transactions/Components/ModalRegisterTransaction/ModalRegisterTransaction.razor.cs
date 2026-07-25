using FluentValidation.Results;
using PersonalFinance.Adapter.Exceptions;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Enums;
using PersonalFinance.Communication.Requests.Transaction;
using PersonalFinance.Communication.Responses.Category;
using PersonalFinance.Communication.Responses.Participant;
using PersonalFinance.Communication.Validators;
using PersonalFinance.Web.Components.AddInput;
using PersonalFinance.Web.Resources.Transactions;
using PersonalFinance.Web.UseState.Category;
using PersonalFinance.Web.UseState.Modal;
using PersonalFinance.Web.Services.SnackbarService;
using PersonalFinance.Web.UseState.Participant;
using PersonalFinance.Web.UseState.Transaction;

namespace PersonalFinance.Web.Pages.Transactions.Components.ModalRegisterTransaction;

public partial class ModalRegisterTransaction
{
    private static readonly List<AddInputOption> TypeOptions =
    [
        new() { Value = ((int)FinancialType.Expense).ToString(), Label = TransactionsResources.Expense },
        new() { Value = ((int)FinancialType.Income).ToString(), Label = TransactionsResources.Income },
    ];

    private List<AddInputOption> ParticipantOptions => ParticipantListState.Value.Participants
        .Select(selector: participant => new AddInputOption { Value = participant.Id.ToString(), Label = participant.Name })
        .ToList();
    
    private List<AddInputOption> CategoryOptions => CategoryListState.Value.Categories
        .Where(predicate: c => ((int)c.Type).ToString() == _typeValue)
        .Select(selector: category => new AddInputOption { Value = category.Id.ToString(), Label = category.Name })
        .ToList();

    private string _participantValue = string.Empty;
    private string _categoryValue = string.Empty;
    private string _typeValue = ((int)FinancialType.Expense).ToString();
    private string _amountText = string.Empty;
    private string _dateText = DateTime.Today.ToString(format: "yyyy-MM-dd");

    private List<string> _errorMessages = [];
    private bool _isSubmitting;

    protected override async Task OnInitializedAsync()
    {
        if (!ParticipantListState.Value.Participants.Any())
        {
            Dispatcher.Dispatch(action: new ParticipantActions.GetAllParticipantsAction());
            GetAllParticipantResponse? response = await PersonalFinanceApi.Participant.GetAll();
            if (response != null)
            {
                Dispatcher.Dispatch(action: new ParticipantActions.GetAllParticipantsSuccessAction(Participants: response.ListParticipants));
            }
        }

        if (!CategoryListState.Value.Categories.Any())
        {
            Dispatcher.Dispatch(action: new CategoryActions.GetAllCategoriesAction());
            GetAllCategoryResponse? response = await PersonalFinanceApi.Category.GetAll();
            if (response != null)
            {
                Dispatcher.Dispatch(action: new CategoryActions.GetAllCategoriesSuccessAction(Categories: response.ListCategories));
            }
        }

        await base.OnInitializedAsync();
    }

    private void OpenAddParticipantModal() => Dispatcher.Dispatch(action: new ModalActions.OpenModalAction(Modal: ModalType.AddParticipant));
    
    private void OpenAddCategoryModal() => Dispatcher.Dispatch(action: new ModalActions.OpenModalAction(Modal: ModalType.AddCategory));

    private async Task HandleRegisterTransaction()
    {
        _errorMessages = [];
        
        RegisterTransactionRequest request = new()
        {
            Date = DateTime.SpecifyKind(value: DateTime.Parse(s: _dateText), kind: DateTimeKind.Utc),
            Type = Enum.TryParse(value: _typeValue, result: out FinancialType type) ? type : FinancialType.Expense,
            Amount = double.Parse(s: _amountText),
            CategoryId = Guid.TryParse(input: _categoryValue, result: out Guid categoryId) ? categoryId : null,
            ParticipantId = Guid.Parse(input: _participantValue),
        };
        
        TransactionValidator validator = new();
        ValidationResult? result = await validator.ValidateAsync(instance: request);

        if (!result.IsValid)
        {
            _errorMessages = result.Errors.Select(selector: error => error.ErrorMessage).ToList();
        }

        _isSubmitting = true;

        try
        {
            TransactionDto response = await PersonalFinanceApi.Transaction.Register(request: request);
            Dispatcher.Dispatch(action: new TransactionActions.RegisterTransactionSuccessAction(Transaction: response));
            HandleClose();
            SnackbarService.Show(message: TransactionsResources.TransactionRegisteredSuccessMessage, severity: SnackbarSeverity.Success);
        }
        catch (ApiException exception) when (exception.ErrorMessages.Count > 0)
        {
            _errorMessages = exception.ErrorMessages.ToList();
        }
        catch
        {
            SnackbarService.Show(message: TransactionsResources.UnknownError, severity: SnackbarSeverity.Error);
        }
        finally
        {
            _isSubmitting = false;
        }
    }

    private void HandleClose()
    {
        _errorMessages = [];
        Dispatcher.Dispatch(action: new ModalActions.CloseModalAction(Modal: ModalType.AddTransaction));
    }
}
