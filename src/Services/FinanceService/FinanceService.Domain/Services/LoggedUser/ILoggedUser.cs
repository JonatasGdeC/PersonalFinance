namespace FinanceService.Domain.Services.LoggedUser;

public interface ILoggedUser
{
    Guid GetUserId();
    string GetUserEmail();
    string GetUserName();
}