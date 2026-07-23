using Microsoft.EntityFrameworkCore;
using PersonalFinance.Domain.Entities;
using PersonalFinance.Domain.Enums;
using PersonalFinance.Domain.Filters;
using PersonalFinance.Domain.ReadModels;
using PersonalFinance.Domain.Repositories.Transaction;
using PersonalFinance.Infrastructure.DataAccess.Utils;

namespace PersonalFinance.Infrastructure.DataAccess.Repositories;

internal class TransactionRepository(PersonalFinanceDbContext context) : ITransactionReadRepository, ITransactionWhiteRepository
{
    public async Task<PagedList<Transaction>> GetAll(Guid userId, TransactionFilter filter)
    {
        IQueryable<Transaction> query = context.Transactions
            .AsNoTracking()
            .Include(navigationPropertyPath: transaction => transaction.Category)
            .Include(navigationPropertyPath: transaction => transaction.Participant)
            .Where(predicate: transaction => transaction.UserId == userId);

        if (!string.IsNullOrWhiteSpace(value: filter.Search))
        {
            string search = filter.Search.ToLower();
            query = query.Where(predicate: transaction =>
                (transaction.Category != null && transaction.Category.Name.ToLower().Contains(search)) ||
                transaction.Participant.Name.ToLower().Contains(search));
        }

        if (filter.CategoryId.HasValue)
        {
            query = query.Where(predicate: transaction => transaction.CategoryId == filter.CategoryId);
        }

        if (filter.TransactionType.HasValue)
        {
            query = query.Where(predicate: transaction => (int)transaction.Type == (int)filter.TransactionType);
        }

        query = filter.ListOrder switch
        {
            ListOrder.Oldest => query.OrderBy(keySelector: transaction => transaction.Date),
            ListOrder.Az => query.OrderBy(keySelector: transaction => transaction.Participant.Name),
            ListOrder.Za => query.OrderByDescending(keySelector: transaction => transaction.Participant.Name),
            ListOrder.Highest => query.OrderByDescending(keySelector: transaction => transaction.Amount),
            ListOrder.Lowest => query.OrderBy(keySelector: transaction => transaction.Amount),
            _ => query.OrderByDescending(keySelector: transaction => transaction.Date)
        };

        return await CreatePageList<Transaction>.Execute(query: query, page: filter.Pagination);
    }

    public async Task<TransactionDashboard> GetDashboard(Guid userId, DateTime date)
    {
        IQueryable<Transaction> query = context.Transactions
            .AsNoTracking()
            .Include(navigationPropertyPath: transaction => transaction.Category)
            .Include(navigationPropertyPath: transaction => transaction.Participant)
            .Where(predicate: transaction => transaction.UserId == userId && transaction.Date.Month == date.Month && transaction.Date.Year == date.Year);

        List<Transaction> lastestTransactions = await query
            .OrderByDescending(keySelector: transaction => transaction.Date)
            .Take(count: 5)
            .ToListAsync();

        double totalIncome = await query
            .Where(predicate: transaction => transaction.Type == TransactionType.Income)
            .SumAsync(selector: transaction => transaction.Amount);
        double totalExpense = await query
            .Where(predicate: transaction => transaction.Type == TransactionType.Expense)
            .SumAsync(selector: transaction => transaction.Amount);

        return new TransactionDashboard
        {
            LastestTransactions = lastestTransactions,
            CurrentBalance = totalIncome - totalExpense,
            TotalIncome = totalIncome,
            TotalExpense = totalExpense
        };
    }

    public async Task<PagedList<Transaction>> GetByCategory(Guid userId, Guid categoryId, DateTime date, Pagination pagination)
    {
        IQueryable<Transaction> query = context.Transactions
            .AsNoTracking()
            .Include(navigationPropertyPath: transaction => transaction.Category)
            .Include(navigationPropertyPath: transaction => transaction.Participant)
            .Where(predicate: transaction => transaction.UserId == userId
                                             && transaction.CategoryId == categoryId
                                             && transaction.Date.Month == date.Month 
                                             && transaction.Date.Year == date.Year);
        
        return await CreatePageList<Transaction>.Execute(query: query, page: pagination);
    }

    public async Task<double> GetTotalAmountByCategory(Guid userId, Guid categoryId, DateTime date)
    {
        return await context.Transactions
            .Where(predicate: transaction => transaction.UserId == userId && transaction.CategoryId == categoryId && transaction.Date.Month == date.Month && transaction.Date.Year == date.Year)
            .SumAsync(selector: transaction => transaction.Amount);
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

    public async Task<Transaction?> GetById(Guid transactionId, Guid userId)
    {
        return await context.Transactions.AsTracking()
            .FirstOrDefaultAsync(predicate: transaction =>
                transaction.Id == transactionId && transaction.UserId == userId);
    }
}