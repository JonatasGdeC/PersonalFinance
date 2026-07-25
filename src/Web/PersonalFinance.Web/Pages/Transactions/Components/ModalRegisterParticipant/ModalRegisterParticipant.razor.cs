using FluentValidation.Results;
using PersonalFinance.Adapter.Exceptions;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Requests.Participant;
using PersonalFinance.Communication.Validators;
using PersonalFinance.Web.Resources.Transactions;
using PersonalFinance.Web.Services.SnackbarService;
using PersonalFinance.Web.UseState.Modal;
using PersonalFinance.Web.UseState.Participant;

namespace PersonalFinance.Web.Pages.Transactions.Components.ModalRegisterParticipant;

public partial class ModalRegisterParticipant
{
    private string _name = string.Empty;
    private string? _image;
    private List<string> _errorMessages = [];
    private bool _isSubmitting;

    private async Task HandleRegisterParticipant()
    {
        _errorMessages = [];
        
        RegisterParticipantRequest request = new()
        {
            Name = _name,
            Image = string.IsNullOrWhiteSpace(value: _image) ? null : _image
        };

        ParticipantValidator validator = new();
        ValidationResult? result = await validator.ValidateAsync(instance: request);

        if (!result.IsValid)
        {
            _errorMessages = result.Errors.Select(selector: error => error.ErrorMessage).ToList();
        }
        
        _isSubmitting = true;

        try
        {
            ParticipantDto participant = await PersonalFinanceApi.Participant.Register(request: request);
            Dispatcher.Dispatch(action: new ParticipantActions.RegisterParticipantSuccessAction(Participant: participant));
            HandleClose();
            SnackbarService.Show(message: TransactionsResources.ParticipantRegisteredSuccessMessage, severity: SnackbarSeverity.Success);
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
        Dispatcher.Dispatch(action: new ModalActions.CloseModalAction(Modal: ModalType.AddParticipant));
    }
}
