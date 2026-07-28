using FinanceService.Domain.Repositories;
using FinanceService.Domain.Repositories.Transaction;
using FinanceService.Domain.Services.LoggedUser;
using PersonalFinance.Exception;
using PersonalFinance.Exception.ExceptionBase;

namespace PersonalFinance.Application.UseCase.Transaction.Delete;
using FinanceService.Domain.Entities;

public class DeleteTransactionUseCase(
    ITransactionWhiteRepository writeRepository,
    ILoggedUser loggedUser,
    IUnitOfWork unitOfWork) : IDeleteTransactionUseCase
{
    public async Task Execute(Guid transactionId)
    {
        User user = await loggedUser.Get();

        Transaction? transaction = await writeRepository.GetById(transactionId: transactionId, userId: user.Id);
        if (transaction == null)
        {
            throw new NotFoundException(message: ResourceErrorMessages.TRANSACTION_NOT_FOUND);
        }

        writeRepository.Delete(transaction: transaction);
        await unitOfWork.Commit();
    }
}
