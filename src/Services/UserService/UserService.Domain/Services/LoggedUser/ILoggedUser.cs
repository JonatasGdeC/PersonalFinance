using UserService.Domain.Entities;

namespace UserService.Domain.Services.LoggedUser;

public interface ILoggedUser
{
    Task<User> Get();
}