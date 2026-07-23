using PersonalFinance.Communication.Dtos;

namespace PersonalFinance.Application.UseCase.Participant.GetById;

public interface IGetParticipantByIdUseCase
{
    Task<ParticipantDto> Execute(Guid id);
}