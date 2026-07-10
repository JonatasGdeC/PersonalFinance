namespace PersonalFinance.Domain.Requests.Participant;

public class GetAllParticipantRequest
{
    public string? Name { get; set; }
    public PageRequest PageRequest { get; set; } = new();
}