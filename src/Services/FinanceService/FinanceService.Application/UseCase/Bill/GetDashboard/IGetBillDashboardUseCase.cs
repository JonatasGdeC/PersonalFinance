using PersonalFinance.Communication.Responses.Bill;

namespace PersonalFinance.Application.UseCase.Bill.GetDashboard;

public interface IGetBillDashboardUseCase
{
    Task<GetBillDashboardResponse> Execute();
}