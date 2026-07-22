using Microsoft.AspNetCore.Components;
using PersonalFinance.Adapter.Exceptions;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Requests.Participant;
using PersonalFinance.Web.Resources.Transactions;

namespace PersonalFinance.Web.Pages.Transactions.Components.AddParticipantModal;

public partial class AddParticipantModal : ComponentBase
{
    [Parameter] public bool IsOpen { get; set; }
    [Parameter] public EventCallback OnClose { get; set; }
    [Parameter] public EventCallback<ParticipantDto> OnRegistered { get; set; }

    private string? _name;
    private string? _image;
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

        RegisterParticipantRequest request = new()
        {
            Name = _name,
            Image = string.IsNullOrWhiteSpace(value: _image) ? null : _image
        };

        try
        {
            ParticipantDto participant = await PersonalFinanceApi.Participant.Register(request: request);

            _name = null;
            _image = null;

            if (OnRegistered.HasDelegate)
            {
                await OnRegistered.InvokeAsync(arg: participant);
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
