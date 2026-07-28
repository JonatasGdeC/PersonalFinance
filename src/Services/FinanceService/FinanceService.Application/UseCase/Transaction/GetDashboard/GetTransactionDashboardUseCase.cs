using AutoMapper;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Responses.Transaction;
using FinanceService.Domain.ReadModels;
using FinanceService.Domain.Repositories.Transaction;
using FinanceService.Domain.Services.LoggedUser;

namespace PersonalFinance.Application.UseCase.Transaction.GetDashboard;

public class GetTransactionDashboardUseCase(
    ITransactionReadRepository readRepository,
    ILoggedUser loggedUser,
    IMapper mapper) : IGetTransactionDashboardUseCase
{
    public async Task<GetTransactionDashboardResponse> Execute(DateTime date)
    {
        Guid userId = loggedUser.GetUserId();

        TransactionDashboard dashboard = await readRepository.GetDashboard(userId: userId, date: date);

        return new GetTransactionDashboardResponse
        {
            LastestTransactions = mapper.Map<List<TransactionDto>>(source: dashboard.LastestTransactions),
            CurrentBalance = dashboard.CurrentBalance,
            TotalIncome = dashboard.TotalIncome,
            TotalExpense = dashboard.TotalExpense
        };
    }
}
