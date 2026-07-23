namespace PersonalFinance.Domain.Repositories.Pot;
using Entities;

public interface IPotWriteRepository
{
    Task Add(Pot pot);
    void Update(Pot pot);
    void Delete(Pot pot);
    Task<Pot?> GetById(Guid potId, Guid userId);
}