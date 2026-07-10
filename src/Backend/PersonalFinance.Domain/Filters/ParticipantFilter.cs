namespace PersonalFinance.Domain.Filters;

public class ParticipantFilter
{
    public string? Name { get; set; }
    public Pagination Pagination { get; set; } = new();
}
