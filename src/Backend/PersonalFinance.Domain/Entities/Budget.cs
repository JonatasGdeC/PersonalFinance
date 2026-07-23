namespace PersonalFinance.Domain.Entities;

public class Budget
{
    public Guid Id { get; set; }
    public double MaximumSpend { get; set; }
    public required string Color { get; set; }
    
    public Guid CategoryId { get; set; }
    public required Category Category { get; set; }
    
    public Guid UserId { get; set; }
    public required User User { get; set; }
}