using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Requests;
using PersonalFinance.Communication.Requests.Transaction;
using PersonalFinance.Communication.Responses.Transaction;

namespace PersonalFinance.Adapter.Interfaces;

public interface ITransactionServiceApi
{
    Task<TransactionDto> Register(RegisterTransactionRequest request);
    Task Update(Guid transactionId, RegisterTransactionRequest request);
    Task Delete(Guid transactionId);
    Task<GetListTransactionsResponse?> GetAll(TransactionFilterRequest request);
    Task<GetTransactionDashboardResponse?> GetDashboard(DateTime date);
    Task<GetListTransactionsResponse?> GetByCategory(Guid categoryId, DateTime date, PaginationRequest pagination);
}
