using MassTransit;
using PersonalFinance.Contracts.Events;
using UserService.Domain.Repositories;
using UserService.Domain.Repositories.User;
using UserService.Domain.Services.LoggedUser;

namespace UserService.Application.UseCase.User.Delete;
using Domain.Entities;

public class DeleteUserUseCase(
    IUserWriteRepository writeRepository,
    ILoggedUser loggedUser,
    IUnitOfWork unitOfWork, 
    IPublishEndpoint publishEndpoint) : IDeleteUserUseCase
{
    public async Task Execute()
    {
        User user = await loggedUser.Get();
        writeRepository.Delete(user: user);
        await unitOfWork.Commit();
        
        await publishEndpoint.Publish(message: new UserDeletedEvent
        {
            UserId = user.Id,
            DeletedAt = DateTime.UtcNow
        });
    }
}