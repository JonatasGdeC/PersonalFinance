namespace PersonalFinance.Domain.Repositories.User;
using Entities;

public interface IUserWriteRepository
{
    Task Add(User user);
    void Update(User user);
    void Delete(User user);
    Task<User?> GetById(Guid id);
}