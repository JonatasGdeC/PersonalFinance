namespace PersonalFinance.Domain.Entities;

public class Participant
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Image { get; set; }
    
    public Guid UserId { get; set; }
    public required User User { get; set; }
}