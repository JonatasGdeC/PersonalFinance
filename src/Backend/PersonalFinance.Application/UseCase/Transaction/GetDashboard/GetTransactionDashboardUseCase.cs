using AutoMapper;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Responses.Transaction;
using PersonalFinance.Domain.ReadModels;
using PersonalFinance.Domain.Repositories.Transaction;
using PersonalFinance.Domain.Services.LoggedUser;

namespace PersonalFinance.Application.UseCase.Transaction.GetDashboard;
using Domain.Entities;

public class GetTransactionDashboardUseCase(
    ITransactionReadRepository readRepository,
    ILoggedUser loggedUser,
    IMapper mapper) : IGetTransactionDashboardUseCase
{
    public async Task<GetTransactionDashboardResponse> Execute(DateTime date)
    {
        User user = await loggedUser.Get();

        TransactionDashboard dashboard = await readRepository.GetDashboard(userId: user.Id, date: date);

        return new GetTransactionDashboardResponse
        {
            LastestTransactions = mapper.Map<List<TransactionDto>>(source: dashboard.LastestTransactions),
            CurrentBalance = dashboard.CurrentBalance,
            TotalIncome = dashboard.TotalIncome,
            TotalExpense = dashboard.TotalExpense
        };
    }
}
