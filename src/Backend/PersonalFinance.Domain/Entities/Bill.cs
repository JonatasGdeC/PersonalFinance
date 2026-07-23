namespace PersonalFinance.Domain.Entities;

public class Bill
{
    public Guid Id { get; set; }
    public DateTime DueDate { get; set; }
    public double Amount { get; set; }
    public int InstallmentsTotal { get; set; }
    public int InstallmentsPaid { get; set; }
    
    public Guid? CategoryId { get; set; }
    public Category? Category { get; set; }
    
    public Guid ParticipantId { get; set; }
    public required Participant Participant { get; set; }
    
    public Guid UserId { get; set; }
    public required User User { get; set; }
}