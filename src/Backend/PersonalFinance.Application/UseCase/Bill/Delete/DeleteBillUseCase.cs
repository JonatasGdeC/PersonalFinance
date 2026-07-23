using PersonalFinance.Domain.Repositories;
using PersonalFinance.Domain.Repositories.Bill;
using PersonalFinance.Domain.Services.LoggedUser;
using PersonalFinance.Exception;
using PersonalFinance.Exception.ExceptionBase;

namespace PersonalFinance.Application.UseCase.Bill.Delete;
using Domain.Entities;

public class DeleteBillUseCase(
    IBillWriteRepository writeRepository,
    ILoggedUser loggedUser,
    IUnitOfWork unitOfWork) : IDeleteBillUseCase
{
    public async Task Execute(Guid billId)
    {
        User user = await loggedUser.Get();

        Bill? bill = await writeRepository.GetById(billId: billId, userId: user.Id);
        if (bill == null)
        {
            throw new NotFoundException(message: ResourceErrorMessages.BILL_NOT_FOUND);
        }

        writeRepository.Delete(bill: bill);
        await unitOfWork.Commit();
    }
}
