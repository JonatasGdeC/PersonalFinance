namespace PersonalFinance.Application.UseCase.Bill.Delete;

public interface IDeleteBillUseCase
{
    Task Execute(Guid billId);
}