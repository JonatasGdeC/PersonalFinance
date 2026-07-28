using PersonalFinance.Communication.Requests.Transaction;
using PersonalFinance.Communication.Responses.Transaction;

namespace PersonalFinance.Application.UseCase.Transaction.GetAll;

public interface IGetAllTransactionUseCase
{
    Task<GetListTransactionsResponse> Execute(TransactionFilterRequest request);
}