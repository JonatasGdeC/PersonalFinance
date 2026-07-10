using Microsoft.EntityFrameworkCore;
using PersonalFinance.Domain.Entities;
using PersonalFinance.Domain.Enums;
using PersonalFinance.Domain.Repositories;
using PersonalFinance.Domain.Repositories.Transaction;
using PersonalFinance.Domain.Requests.Transaction;
using PersonalFinance.Infrastructure.DataAccess.Utils;

namespace PersonalFinance.Infrastructure.DataAccess.Repositories;

internal class TransactionRepository(PersonalFinanceDbContext context) : ITransactionReadRepository, ITransactionWhiteRepository
{
    public async Task<PagedList<Transaction>> GetAll(Guid userId, GetAllTransactionRequest request)
    {
        IQueryable<Transaction> query = context.Transactions
            .AsNoTracking()
            .Include(navigationPropertyPath: transaction => transaction.Category)
            .Include(navigationPropertyPath: transaction => transaction.Participant)
            .Where(predicate: transaction => transaction.UserId == userId);

        if (!string.IsNullOrWhiteSpace(value: request.Search))
        {
            string search = request.Search.ToLower();
            query = query.Where(predicate: transaction =>
                (transaction.Category != null && transaction.Category.Name.ToLower().Contains(search)) ||
                transaction.Participant.Name.ToLower().Contains(search));
        }

        if (request.CategoryId.HasValue)
        {
            query = query.Where(predicate: transaction => transaction.CategoryId == request.CategoryId);
        }

        if (request.TransactionType.HasValue)
        {
            query = query.Where(predicate: transaction => (int)transaction.Type == (int)request.TransactionType);
        }

        query = request.ListOrder switch
        {
            ListOrder.Oldest  => query.OrderBy(keySelector: transaction => transaction.Date),
            ListOrder.Az      => query.OrderBy(keySelector: transaction => transaction.Participant.Name),
            ListOrder.Za      => query.OrderByDescending(keySelector: transaction => transaction.Participant.Name),
            ListOrder.Highest => query.OrderByDescending(keySelector: transaction => transaction.Amount),
            ListOrder.Lowest  => query.OrderBy(keySelector: transaction => transaction.Amount),
            _                 => query.OrderByDescending(keySelector: transaction => transaction.Date)
        };

        return await CreatePageList<Transaction>.Execute(query: query, page: request.PageRequest);
    }

    public async Task Add(Transaction transaction)
    {
        await context.Transactions.AddAsync(entity: transaction);
    }

    public void Update(Transaction transaction)
    {
        context.Transactions.Update(entity: transaction);
    }

    public void Delete(Transaction transaction)
    {
        context.Transactions.Remove(entity: transaction);
    }

    public async Task<Transaction?> GetById(long transactionId, Guid userId)
    {
        return await context.Transactions.AsTracking()
            .FirstOrDefaultAsync(predicate: transaction =>
                transaction.Id == transactionId && transaction.UserId == userId);
    }
}