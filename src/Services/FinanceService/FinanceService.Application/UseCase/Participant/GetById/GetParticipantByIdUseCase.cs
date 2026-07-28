using AutoMapper;
using PersonalFinance.Communication.Dtos;
using FinanceService.Domain.Repositories.Participant;
using FinanceService.Domain.Services.LoggedUser;
using PersonalFinance.Exception;
using PersonalFinance.Exception.ExceptionBase;

namespace PersonalFinance.Application.UseCase.Participant.GetById;
using FinanceService.Domain.Entities;

public class GetParticipantByIdUseCase(
    IParticipantWriteRepository writeRepository,
    ILoggedUser loggedUser,
    IMapper mapper) : IGetParticipantByIdUseCase
{
    public async Task<ParticipantDto> Execute(Guid id)
    {
        Guid userId = loggedUser.GetUserId();

        Participant? participant = await writeRepository.GetById(participantId: id, userId: userId);
        if (participant == null)
        {
            throw new NotFoundException(message: ResourceErrorMessages.PARTICIPANT_NOT_FOUND);
        }

        return mapper.Map<ParticipantDto>(source: participant);
    }
}
