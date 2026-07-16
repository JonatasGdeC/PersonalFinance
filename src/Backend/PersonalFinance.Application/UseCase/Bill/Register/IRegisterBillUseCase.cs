using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Requests.Bill;

namespace PersonalFinance.Application.UseCase.Bill.Register;

public interface IRegisterBillUseCase
{
    Task<BudgetDto> Execute(RegisterBillRequest request);
}