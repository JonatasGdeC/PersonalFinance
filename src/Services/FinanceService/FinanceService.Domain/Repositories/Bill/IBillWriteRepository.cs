namespace FinanceService.Domain.Repositories.Bill;
using Entities;

public interface IBillWriteRepository
{
    Task Add(Bill bill);
    void Update(Bill bill);
    void Delete(Bill bill);
    Task<Bill?> GetById(Guid billId, Guid userId);
    Task DeleteByUserId(Guid userId);
}