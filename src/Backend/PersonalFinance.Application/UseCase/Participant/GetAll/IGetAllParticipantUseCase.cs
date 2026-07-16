using PersonalFinance.Communication.Responses.Participant;

namespace PersonalFinance.Application.UseCase.Participant.GetAll;

public interface IGetAllParticipantUseCase
{
    Task<GetAllParticipantResponse> Execute();
}