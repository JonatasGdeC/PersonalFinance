namespace PersonalFinance.Domain.Entities;

public class Bill
{
    public long Id { get; set; }
    public DateTime DueDate { get; set; }
    public double Amount { get; set; }
    public int InstallmentsTotal { get; set; }
    public int InstallmentsPaid { get; set; }
    
    public long? CategoryId { get; set; }
    public Category? Category { get; set; }
    
    public long ParticipantId { get; set; }
    public required Participant Participant { get; set; }
    
    public Guid UserId { get; set; }
    public required User User { get; set; }
}