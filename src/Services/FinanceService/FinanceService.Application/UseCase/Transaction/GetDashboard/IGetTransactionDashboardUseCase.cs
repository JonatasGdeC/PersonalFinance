using PersonalFinance.Communication.Responses.Transaction;

namespace PersonalFinance.Application.UseCase.Transaction.GetDashboard;

public interface IGetTransactionDashboardUseCase
{
    Task<GetTransactionDashboardResponse> Execute(DateTime date);
}