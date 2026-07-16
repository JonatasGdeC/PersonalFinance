using PersonalFinance.Communication.Enums;

namespace PersonalFinance.Communication.Requests.Transaction;

public record RegisterTransactionRequest
{
    public DateTime Date { get; set; }
    public TransactionType Type { get; set; }
    public double Amount { get; set; }
    public long? CategoryId { get; set; }
    public long ParticipantId { get; set; }
}