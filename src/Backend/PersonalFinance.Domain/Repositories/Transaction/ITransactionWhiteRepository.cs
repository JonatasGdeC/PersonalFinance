namespace PersonalFinance.Domain.Repositories.Transaction;
using Entities;

public interface ITransactionWhiteRepository
{
    Task Add(Transaction transaction);
    void Update(Transaction transaction);
    void Delete(Transaction transaction);
    Task<Transaction?> GetById(Guid transactionId, Guid userId);
}