using FluentValidation.Results;
using PersonalFinance.Communication.Requests.Participant;
using PersonalFinance.Communication.Validators;
using FinanceService.Domain.Repositories;
using FinanceService.Domain.Repositories.Participant;
using FinanceService.Domain.Services.LoggedUser;
using PersonalFinance.Exception;
using PersonalFinance.Exception.ExceptionBase;

namespace PersonalFinance.Application.UseCase.Participant.Update;
using FinanceService.Domain.Entities;

public class UpdateParticipantUseCase(
    IParticipantWriteRepository writeRepository,
    ILoggedUser loggedUser,
    IUnitOfWork unitOfWork) : IUpdateParticipantUseCase
{
    public async Task Execute(Guid participantId, RegisterParticipantRequest request)
    {
        await Validate(request: request);

        Guid userId = loggedUser.GetUserId();

        Participant? participant = await writeRepository.GetById(participantId: participantId, userId: userId);
        if (participant == null)
        {
            throw new NotFoundException(message: ResourceErrorMessages.PARTICIPANT_NOT_FOUND);
        }

        participant.Name = request.Name;
        participant.Image = request.Image;

        writeRepository.Update(participant: participant);
        await unitOfWork.Commit();
    }

    private async Task Validate(RegisterParticipantRequest request)
    {
        ParticipantValidator validator = new();
        ValidationResult? result = await validator.ValidateAsync(instance: request);

        if (!result.IsValid)
        {
            List<string> errors = result.Errors.Select(selector: error => error.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorsMessages: errors);
        }
    }
}
