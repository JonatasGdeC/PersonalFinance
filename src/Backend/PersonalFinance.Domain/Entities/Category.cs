namespace PersonalFinance.Domain.Entities;

public class Category
{
    public long Id { get; set; }
    public required string Name { get; set; }
    
    public Guid UserId { get; set; }
    public required User User { get; set; }
}