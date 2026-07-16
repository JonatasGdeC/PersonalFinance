using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Requests.Participant;

namespace PersonalFinance.Application.UseCase.Participant.Register;

public interface IRegisterParticipantUseCase
{
    Task<ParticipantDto> Execute(RegisterParticipantRequest request);
}