namespace PersonalFinance.Domain.Repositories.Pot;
using Entities;

public interface IPotReadRepository
{
    Task<List<Pot>> GetAll(Guid userId);
}