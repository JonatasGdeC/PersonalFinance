namespace PersonalFinance.Communication.Requests.Pot;

public record RegisterPotRequest
{
    public required string Name { get; set; }
    public double CurrentAmount { get; set; }
    public double Target { get; set; }
    public required string Color { get; set; }
}