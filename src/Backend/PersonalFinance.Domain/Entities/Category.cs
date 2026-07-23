using PersonalFinance.Domain.Enums;

namespace PersonalFinance.Domain.Entities;

public class Category
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required TransactionType Type  { get; set; }
    
    public Guid UserId { get; set; }
    public required User User { get; set; }
}