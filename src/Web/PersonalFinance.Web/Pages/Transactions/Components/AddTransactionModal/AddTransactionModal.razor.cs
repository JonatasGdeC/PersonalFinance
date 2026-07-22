using Microsoft.AspNetCore.Components;
using PersonalFinance.Adapter.Exceptions;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Enums;
using PersonalFinance.Communication.Requests.Transaction;
using PersonalFinance.Communication.Responses.Participant;
using PersonalFinance.Web.Components.AddInput;
using PersonalFinance.Web.Resources.Transactions;

namespace PersonalFinance.Web.Pages.Transactions.Components.AddTransactionModal;

public partial class AddTransactionModal : ComponentBase
{
    private static readonly List<AddInputOption> TypeOptions =
    [
        new() { Value = ((int)TransactionType.Expense).ToString(), Label = TransactionsResources.Expense },
        new() { Value = ((int)TransactionType.Income).ToString(), Label = TransactionsResources.Income },
    ];

    [Parameter] public required List<AddInputOption> CategoryOptions { get; set; }
    [Parameter] public bool IsOpen { get; set; }
    [Parameter] public EventCallback OnClose { get; set; }
    [Parameter] public EventCallback OnRegistered { get; set; }
    [Parameter] public EventCallback<CategoryDto> OnCategoryRegistered { get; set; }

    private List<AddInputOption> _participantOptions = [];
    private bool _hasLoadedParticipants;
    private bool _isAddParticipantModalOpen;

    private List<AddInputOption> _categoryOptions = [];
    private bool _hasSeededCategories;
    private bool _isAddCategoryModalOpen;

    private string? _participantValue;
    private string? _categoryValue;
    private string _typeValue = ((int)TransactionType.Expense).ToString();
    private string? _amountText;
    private string _dateText = DateTime.Today.ToString(format: "yyyy-MM-dd");

    private List<string> _errorMessages = [];
    private bool _isSubmitting;

    protected override async Task OnParametersSetAsync()
    {
        if (!_hasSeededCategories && CategoryOptions.Count > 0)
        {
            _categoryOptions = [.. CategoryOptions];
            _hasSeededCategories = true;
        }

        if (IsOpen && !_hasLoadedParticipants)
        {
            await LoadParticipants();
        }
    }

    private async Task LoadParticipants()
    {
        GetAllParticipantResponse? response = await PersonalFinanceApi.Participant.GetAll();

        _participantOptions = (response?.ListParticipants ?? [])
            .Select(selector: participant => new AddInputOption { Value = participant.Id.ToString(), Label = participant.Name })
            .ToList();

        _hasLoadedParticipants = true;
    }

    private void OpenAddParticipantModal() => _isAddParticipantModalOpen = true;

    private void CloseAddParticipantModal() => _isAddParticipantModalOpen = false;

    private void HandleParticipantRegistered(ParticipantDto participant)
    {
        _participantOptions.Add(item: new AddInputOption { Value = participant.Id.ToString(), Label = participant.Name });
        _participantValue = participant.Id.ToString();
        _isAddParticipantModalOpen = false;
    }

    private void OpenAddCategoryModal() => _isAddCategoryModalOpen = true;

    private void CloseAddCategoryModal() => _isAddCategoryModalOpen = false;

    private async Task HandleCategoryRegistered(CategoryDto category)
    {
        _categoryOptions.Add(item: new AddInputOption { Value = category.Id.ToString(), Label = category.Name });
        _categoryValue = category.Id.ToString();
        _isAddCategoryModalOpen = false;

        if (OnCategoryRegistered.HasDelegate)
        {
            await OnCategoryRegistered.InvokeAsync(arg: category);
        }
    }

    private async Task HandleSubmit()
    {
        _errorMessages = [];

        bool isAmountValid = double.TryParse(s: _amountText, result: out double amount) && amount > 0;
        bool isDateValid = DateTime.TryParse(s: _dateText, result: out DateTime date);
        bool isParticipantValid = long.TryParse(s: _participantValue, result: out long participantId);

        if (!isAmountValid || !isDateValid || !isParticipantValid)
        {
            _errorMessages = [TransactionsResources.RequiredFieldsError];
            return;
        }

        _isSubmitting = true;

        RegisterTransactionRequest request = new()
        {
            Date = date,
            Type = Enum.TryParse(value: _typeValue, result: out TransactionType type) ? type : TransactionType.Expense,
            Amount = amount,
            CategoryId = long.TryParse(s: _categoryValue, result: out long categoryId) ? categoryId : null,
            ParticipantId = participantId
        };

        try
        {
            await PersonalFinanceApi.Transaction.Register(request: request);
            ResetForm();

            if (OnRegistered.HasDelegate)
            {
                await OnRegistered.InvokeAsync();
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

    private void ResetForm()
    {
        _participantValue = null;
        _categoryValue = null;
        _typeValue = ((int)TransactionType.Expense).ToString();
        _amountText = null;
        _dateText = DateTime.Today.ToString(format: "yyyy-MM-dd");
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
