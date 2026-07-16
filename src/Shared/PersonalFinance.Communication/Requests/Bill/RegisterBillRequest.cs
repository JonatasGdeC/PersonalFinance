namespace PersonalFinance.Communication.Requests.Bill;

public record RegisterBillRequest
{
    public DateTime DueDate { get; set; }
    public double Amount { get; set; }
    public int InstallmentsTotal { get; set; }
    public int InstallmentsPaid { get; set; }
    public long? CategoryId { get; set; }
    public long ParticipantId { get; set; }
}