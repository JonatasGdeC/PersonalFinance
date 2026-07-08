namespace PersonalFinance.Domain.Repositories.User;
using PersonalFinance.Domain.Entities;

public interface IUserReadRepository
{
    Task<User?> GetByEmail(string email);
}