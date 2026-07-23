using PersonalFinance.Communication.Requests.Bill;

namespace PersonalFinance.Application.UseCase.Bill.Update;

public interface IUpdateBillUseCase
{
    Task Execute(Guid billId, RegisterBillRequest request);
}