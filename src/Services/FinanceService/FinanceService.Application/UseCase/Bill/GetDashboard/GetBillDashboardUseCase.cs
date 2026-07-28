using PersonalFinance.Communication.Responses.Bill;
using FinanceService.Domain.ReadModels;
using FinanceService.Domain.Repositories.Bill;
using FinanceService.Domain.Services.LoggedUser;

namespace PersonalFinance.Application.UseCase.Bill.GetDashboard;

public class GetBillDashboardUseCase(
    IBillReadRepository readRepository,
    ILoggedUser loggedUser) : IGetBillDashboardUseCase
{
    public async Task<GetBillDashboardResponse> Execute()
    {
        Guid userId = loggedUser.GetUserId();

        BillDashboard dashboard = await readRepository.GetDashboard(userId: userId);

        return new GetBillDashboardResponse
        {
            Total = dashboard.Total,
            Paid = dashboard.Paid,
            Upcoming = dashboard.Upcoming,
            DueSoon = dashboard.DueSoon
        };
    }
}
