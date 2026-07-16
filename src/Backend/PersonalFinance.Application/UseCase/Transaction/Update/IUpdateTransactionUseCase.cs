using PersonalFinance.Communication.Requests.Transaction;

namespace PersonalFinance.Application.UseCase.Transaction.Update;

public interface IUpdateTransactionUseCase
{
    Task Execute(long transaction, RegisterTransactionRequest request);
}