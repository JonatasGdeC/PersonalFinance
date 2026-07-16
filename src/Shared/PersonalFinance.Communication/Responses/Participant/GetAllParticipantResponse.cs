using PersonalFinance.Communication.Dtos;

namespace PersonalFinance.Communication.Responses.Participant;

public record GetAllParticipantResponse
{
    public List<ParticipantDto> ListParticipants { get; init; } = [];
}