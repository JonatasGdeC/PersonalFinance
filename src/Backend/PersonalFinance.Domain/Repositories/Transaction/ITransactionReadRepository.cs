using PersonalFinance.Domain.Requests.Transaction;
using PersonalFinance.Domain.Response;
using PersonalFinance.Domain.Response.Transaction;

namespace PersonalFinance.Domain.Repositories.Transaction;
using Entities;

public interface ITransactionReadRepository
{
    Task<PagedListResponse<Transaction>> GetAll(Guid userId, GetAllTransactionRequest request);
    Task<GetTransactionDashboardResponse> GetDashboard(Guid userId, DateTime date);
}