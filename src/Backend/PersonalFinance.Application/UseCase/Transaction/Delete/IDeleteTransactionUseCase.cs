namespace PersonalFinance.Application.UseCase.Transaction.Delete;

public interface IDeleteTransactionUseCase
{
    Task Execute(long transactionId);
}