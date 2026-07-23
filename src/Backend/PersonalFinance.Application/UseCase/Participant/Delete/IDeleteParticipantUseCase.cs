namespace PersonalFinance.Application.UseCase.Participant.Delete;

public interface IDeleteParticipantUseCase
{
    Task Execute(Guid participantId);
}