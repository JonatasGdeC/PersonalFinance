using FinanceService.Domain.Repositories;
using FinanceService.Domain.Repositories.Bill;
using FinanceService.Domain.Services.LoggedUser;
using PersonalFinance.Exception;
using PersonalFinance.Exception.ExceptionBase;

namespace PersonalFinance.Application.UseCase.Bill.Delete;
using FinanceService.Domain.Entities;

public class DeleteBillUseCase(
    IBillWriteRepository writeRepository,
    ILoggedUser loggedUser,
    IUnitOfWork unitOfWork) : IDeleteBillUseCase
{
    public async Task Execute(Guid billId)
    {
        Guid userId = loggedUser.GetUserId();

        Bill? bill = await writeRepository.GetById(billId: billId, userId: userId);
        if (bill == null)
        {
            throw new NotFoundException(message: ResourceErrorMessages.BILL_NOT_FOUND);
        }

        writeRepository.Delete(bill: bill);
        await unitOfWork.Commit();
    }
}
