namespace PersonalFinance.Domain.Entities;

public class Pot
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public double CurrentAmount { get; set; }
    public double Target { get; set; }
    public required string Color { get; set; }

    public Guid UserId { get; set; }
    public required User User { get; set; }
}