using FluentValidation.Results;
using PersonalFinance.Adapter.Exceptions;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Requests.Bill;
using PersonalFinance.Communication.Responses.Category;
using PersonalFinance.Communication.Responses.Participant;
using PersonalFinance.Communication.Validators;
using PersonalFinance.Web.Components.AddInput;
using PersonalFinance.Web.Resources.Bills;
using PersonalFinance.Web.Services.SnackbarService;
using PersonalFinance.Web.UseState.Bill;
using PersonalFinance.Web.UseState.Category;
using PersonalFinance.Web.UseState.Modal;
using PersonalFinance.Web.UseState.Participant;

namespace PersonalFinance.Web.Pages.Bills.Components.ModalRegisterBill;

public partial class ModalRegisterBill
{
    private List<AddInputOption> ParticipantOptions => ParticipantListState.Value.Participants
        .Select(selector: participant => new AddInputOption { Value = participant.Id.ToString(), Label = participant.Name })
        .ToList();

    private List<AddInputOption> CategoryOptions => CategoryListState.Value.Categories
        .Select(selector: category => new AddInputOption { Value = category.Id.ToString(), Label = category.Name })
        .ToList();

    private string _participantValue = string.Empty;
    private string _categoryValue = string.Empty;
    private string _amountText = string.Empty;
    private string _dueDateText = DateTime.Today.ToString(format: "yyyy-MM-dd");
    private string _installmentsTotalText = string.Empty;
    private string _installmentsPaidText = "0";

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

    private async Task HandleRegisterBill()
    {
        _errorMessages = [];

        RegisterBillRequest request = new()
        {
            DueDate = DateTime.SpecifyKind(value: DateTime.Parse(s: _dueDateText), kind: DateTimeKind.Utc),
            Amount = double.TryParse(s: _amountText, result: out double amount) ? amount : 0,
            InstallmentsTotal = int.TryParse(s: _installmentsTotalText, result: out int installmentsTotal) ? installmentsTotal : 0,
            InstallmentsPaid = int.TryParse(s: _installmentsPaidText, result: out int installmentsPaid) ? installmentsPaid : 0,
            CategoryId = Guid.TryParse(input: _categoryValue, result: out Guid categoryId) ? categoryId : null,
            ParticipantId = Guid.TryParse(input: _participantValue, result: out Guid participantId) ? participantId : Guid.Empty
        };

        BillValidator validator = new();
        ValidationResult? result = await validator.ValidateAsync(instance: request);

        if (!result.IsValid)
        {
            _errorMessages = result.Errors.Select(selector: error => error.ErrorMessage).ToList();
            return;
        }

        _isSubmitting = true;

        try
        {
            BillDto bill = await PersonalFinanceApi.Bill.Register(request: request);
            Dispatcher.Dispatch(action: new BillActions.RegisterBillSuccessAction(Bill: bill));
            ResetForm();
            HandleClose();
            SnackbarService.Show(message: BillsResources.BillRegisteredSuccessMessage, severity: SnackbarSeverity.Success);
        }
        catch (ApiException exception) when (exception.ErrorMessages.Count > 0)
        {
            _errorMessages = exception.ErrorMessages.ToList();
        }
        catch
        {
            SnackbarService.Show(message: BillsResources.UnknownError, severity: SnackbarSeverity.Error);
        }
        finally
        {
            _isSubmitting = false;
        }
    }

    private void ResetForm()
    {
        _participantValue = string.Empty;
        _categoryValue = string.Empty;
        _amountText = string.Empty;
        _dueDateText = DateTime.Today.ToString(format: "yyyy-MM-dd");
        _installmentsTotalText = string.Empty;
        _installmentsPaidText = "0";
    }

    private void HandleClose()
    {
        _errorMessages = [];
        Dispatcher.Dispatch(action: new ModalActions.CloseModalAction(Modal: ModalType.AddBill));
    }
}
