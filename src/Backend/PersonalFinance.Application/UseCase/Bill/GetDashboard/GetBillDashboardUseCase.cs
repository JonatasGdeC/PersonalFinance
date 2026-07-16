using PersonalFinance.Communication.Responses.Bill;
using PersonalFinance.Domain.ReadModels;
using PersonalFinance.Domain.Repositories.Bill;
using PersonalFinance.Domain.Services.LoggedUser;

namespace PersonalFinance.Application.UseCase.Bill.GetDashboard;
using Domain.Entities;

public class GetBillDashboardUseCase(
    IBillReadRepository readRepository,
    ILoggedUser loggedUser) : IGetBillDashboardUseCase
{
    public async Task<GetBillDashboardResponse> Execute()
    {
        User user = await loggedUser.Get();

        BillDashboard dashboard = await readRepository.GetDashboard(userId: user.Id);

        return new GetBillDashboardResponse
        {
            Total = dashboard.Total,
            Paid = dashboard.Paid,
            Upcoming = dashboard.Upcoming,
            DueSoon = dashboard.DueSoon
        };
    }
}
