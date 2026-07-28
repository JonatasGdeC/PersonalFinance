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
        Guid userId = loggedUser.GetUserId();

        Transaction? transaction = await writeRepository.GetById(transactionId: transactionId, userId: userId);
        if (transaction == null)
        {
            throw new NotFoundException(message: ResourceErrorMessages.TRANSACTION_NOT_FOUND);
        }

        writeRepository.Delete(transaction: transaction);
        await unitOfWork.Commit();
    }
}
