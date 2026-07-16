namespace PersonalFinance.Communication.Requests.Participant;

public record RegisterParticipantRequest
{
    public required string Name { get; set; }
    public string? Image { get; set; }
}