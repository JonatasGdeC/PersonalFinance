using PersonalFinance.Domain.Repositories;
using PersonalFinance.Domain.Repositories.Participant;
using PersonalFinance.Domain.Services.LoggedUser;
using PersonalFinance.Exception;
using PersonalFinance.Exception.ExceptionBase;

namespace PersonalFinance.Application.UseCase.Participant.Delete;
using Domain.Entities;

public class DeleteParticipantUseCase(
    IParticipantWriteRepository writeRepository,
    ILoggedUser loggedUser,
    IUnitOfWork unitOfWork) : IDeleteParticipantUseCase
{
    public async Task Execute(long participantId)
    {
        User user = await loggedUser.Get();

        Participant? participant = await writeRepository.GetById(participantId: participantId, userId: user.Id);
        if (participant == null)
        {
            throw new NotFoundException(message: ResourceErrorMessages.PARTICIPANT_NOT_FOUND);
        }

        writeRepository.Delete(participant: participant);
        await unitOfWork.Commit();
    }
}
