using FinanceService.Domain.Repositories;
using FinanceService.Domain.Repositories.User;
using FinanceService.Domain.Services.LoggedUser;

namespace PersonalFinance.Application.UseCase.User.Delete;
using FinanceService.Domain.Entities;

public class DeleteUserUseCase(
    IUserWriteRepository writeRepository,
    ILoggedUser loggedUser,
    IUnitOfWork unitOfWork) : IDeleteUserUseCase
{
    public async Task Execute()
    {
        User user = await loggedUser.Get();
        writeRepository.Delete(user: user);
        await unitOfWork.Commit();
    }
}