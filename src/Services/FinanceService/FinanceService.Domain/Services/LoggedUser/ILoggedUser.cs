using FinanceService.Domain.Entities;

namespace FinanceService.Domain.Services.LoggedUser;

public interface ILoggedUser
{
    Task<User> Get();
    Guid GetUserId();
    string GetUserEmail();
    string GetUserName();
}