using PersonalFinance.Domain.Enums;

namespace PersonalFinance.Domain.Entities;

public class Transaction
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public TransactionType Type { get; set; }
    public double Amount { get; set; }
    
    public Guid? CategoryId { get; set; }
    public Category? Category { get; set; }
    
    public Guid ParticipantId { get; set; }
    public required Participant Participant { get; set; }
    
    public Guid UserId { get; set; }
    public required User User { get; set; }
}