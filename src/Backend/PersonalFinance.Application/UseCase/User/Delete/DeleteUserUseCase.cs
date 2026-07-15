using PersonalFinance.Domain.Repositories;
using PersonalFinance.Domain.Repositories.User;
using PersonalFinance.Domain.Services.LoggedUser;

namespace PersonalFinance.Application.UseCase.User.Delete;
using Domain.Entities;

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