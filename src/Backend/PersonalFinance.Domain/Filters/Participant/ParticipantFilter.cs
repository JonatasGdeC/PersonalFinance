namespace PersonalFinance.Domain.Filters.Participant;

public class ParticipantFilter
{
    public string? Name { get; set; }
    public Pagination Pagination { get; set; } = new();
}
