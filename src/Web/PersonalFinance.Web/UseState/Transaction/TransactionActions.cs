using PersonalFinance.Communication.Dtos;

namespace PersonalFinance.Web.UseState.Transaction;

public abstract class TransactionActions
{
    public record DeleteTransactionSuccessAction(Guid TransactionId);
    public record GetAllTransactionsAction;
    public record GetAllTransactionsSuccessAction(List<TransactionDto> Transactions);
    public record GetTransactionByIdAction(Guid TransactionId);
    public record GetTransactionByIdSuccessAction(TransactionDto Transaction);
    public record RegisterTransactionSuccessAction(TransactionDto Transaction);
    public record UpdateTransactionSuccessAction(TransactionDto Transaction);
}
