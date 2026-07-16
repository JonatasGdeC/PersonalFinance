using PersonalFinance.Communication.Requests.Participant;

namespace PersonalFinance.Application.UseCase.Participant.Update;

public interface IUpdateParticipantUseCase
{
    Task Execute(long participantId, RegisterParticipantRequest request);
}