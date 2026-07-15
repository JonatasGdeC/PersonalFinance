using PersonalFinance.Domain.Entities;

namespace PersonalFinance.Domain.Services.LoggedUser;

public interface ILoggedUser
{
    Task<User> Get();
}