using AutoMapper;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Domain.Repositories.Participant;
using PersonalFinance.Domain.Services.LoggedUser;
using PersonalFinance.Exception;
using PersonalFinance.Exception.ExceptionBase;

namespace PersonalFinance.Application.UseCase.Participant.GetById;
using Domain.Entities;

public class GetParticipantByIdUseCase(
    IParticipantWriteRepository writeRepository,
    ILoggedUser loggedUser,
    IMapper mapper) : IGetParticipantByIdUseCase
{
    public async Task<ParticipantDto> Execute(long id)
    {
        User user = await loggedUser.Get();

        Participant? participant = await writeRepository.GetById(participantId: id, userId: user.Id);
        if (participant == null)
        {
            throw new NotFoundException(message: ResourceErrorMessages.PARTICIPANT_NOT_FOUND);
        }

        return mapper.Map<ParticipantDto>(source: participant);
    }
}
