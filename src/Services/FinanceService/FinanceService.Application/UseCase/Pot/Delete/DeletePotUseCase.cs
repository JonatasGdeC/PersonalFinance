using FinanceService.Domain.Repositories;
using FinanceService.Domain.Repositories.Pot;
using FinanceService.Domain.Services.LoggedUser;
using PersonalFinance.Exception;
using PersonalFinance.Exception.ExceptionBase;

namespace PersonalFinance.Application.UseCase.Pot.Delete;
using FinanceService.Domain.Entities;

public class DeletePotUseCase(
    IPotWriteRepository writeRepository,
    ILoggedUser loggedUser,
    IUnitOfWork unitOfWork) : IDeletePotUseCase
{
    public async Task Execute(Guid potId)
    {
        Guid userId = loggedUser.GetUserId();

        Pot? pot = await writeRepository.GetById(potId: potId, userId: userId);
        if (pot == null)
        {
            throw new NotFoundException(message: ResourceErrorMessages.POT_NOT_FOUND);
        }

        writeRepository.Delete(pot: pot);
        await unitOfWork.Commit();
    }
}
