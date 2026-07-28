using PersonalFinance.Communication.Requests.Bill;
using PersonalFinance.Communication.Responses.Bill;

namespace PersonalFinance.Application.UseCase.Bill.GetAll;

public interface IGetAllBillUseCase
{
    Task<GetAllBillResponse> Execute(BillFilterRequest filter);
}