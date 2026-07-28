using FinanceService.Domain.Repositories;
using FinanceService.Domain.Repositories.Participant;
using FinanceService.Domain.Services.LoggedUser;
using PersonalFinance.Exception;
using PersonalFinance.Exception.ExceptionBase;

namespace PersonalFinance.Application.UseCase.Participant.Delete;
using FinanceService.Domain.Entities;

public class DeleteParticipantUseCase(
    IParticipantWriteRepository writeRepository,
    ILoggedUser loggedUser,
    IUnitOfWork unitOfWork) : IDeleteParticipantUseCase
{
    public async Task Execute(Guid participantId)
    {
        Guid userId = loggedUser.GetUserId();

        Participant? participant = await writeRepository.GetById(participantId: participantId, userId: userId);
        if (participant == null)
        {
            throw new NotFoundException(message: ResourceErrorMessages.PARTICIPANT_NOT_FOUND);
        }

        writeRepository.Delete(participant: participant);
        await unitOfWork.Commit();
    }
}
