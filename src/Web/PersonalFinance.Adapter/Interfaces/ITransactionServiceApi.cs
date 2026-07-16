using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Requests;
using PersonalFinance.Communication.Requests.Transaction;
using PersonalFinance.Communication.Responses.Transaction;

namespace PersonalFinance.Adapter.Interfaces;

public interface ITransactionServiceApi
{
    Task<TransactionDto> Register(RegisterTransactionRequest request);
    Task Update(long transactionId, RegisterTransactionRequest request);
    Task Delete(long transactionId);
    Task<GetListTransactionsResponse?> GetAll(TransactionFilterRequest request);
    Task<GetTransactionDashboardResponse?> GetDashboard(DateTime date);
    Task<GetListTransactionsResponse?> GetByCategory(long categoryId, DateTime date, PaginationRequest pagination);
}
