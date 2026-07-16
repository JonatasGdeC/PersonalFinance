using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Requests.Participant;
using PersonalFinance.Communication.Responses.Participant;

namespace PersonalFinance.Adapter.Interfaces;

public interface IParticipantServiceApi
{
    Task<ParticipantDto> Register(RegisterParticipantRequest request);
    Task Update(long participantId, RegisterParticipantRequest request);
    Task Delete(long participantId);
    Task<GetAllParticipantResponse?> GetAll();
    Task<ParticipantDto?> GetById(long participantId);
}
