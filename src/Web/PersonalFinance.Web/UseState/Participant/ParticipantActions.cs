using PersonalFinance.Communication.Dtos;

namespace PersonalFinance.Web.UseState.Participant;

public abstract class ParticipantActions
{
    public record DeleteParticipantSuccessAction(Guid ParticipantId);
    public record GetAllParticipantsAction;
    public record GetAllParticipantsSuccessAction(List<ParticipantDto> Participants);
    public record GetParticipantByIdAction(Guid ParticipantId);
    public record GetParticipantByIdSuccessAction(ParticipantDto Participant);
    public record RegisterParticipantSuccessAction(ParticipantDto Participant);
    public record UpdateParticipantSuccessAction(ParticipantDto Participant);
}
