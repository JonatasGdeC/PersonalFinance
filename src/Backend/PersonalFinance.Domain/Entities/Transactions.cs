using System;
using PersonalFinance.Domain.Enums;

namespace PersonalFinance.Domain.Entities;

public class Transactions
{
    public long Id { get; set; }
    public DateTime Date { get; set; }
    public TransactionType Type { get; set; }
    public double Amount { get; set; }
    
    public long? CategoryId { get; set; }
    public Category? Category { get; set; }
    
    public long ParticipantId { get; set; }
    public required Participant Participant { get; set; }
    
    public Guid UserId { get; set; }
    public required User User { get; set; }
}