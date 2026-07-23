using Microsoft.EntityFrameworkCore;
using PersonalFinance.Domain.Entities;
using PersonalFinance.Domain.Enums;
using PersonalFinance.Domain.Filters;
using PersonalFinance.Domain.ReadModels;
using PersonalFinance.Domain.Repositories.Bill;
using PersonalFinance.Infrastructure.DataAccess.Utils;

namespace PersonalFinance.Infrastructure.DataAccess.Repositories;

internal class BillRepository(PersonalFinanceDbContext context) : IBillReadRepository, IBillWriteRepository
{
    public async Task<PagedList<Bill>> GetAll(Guid userId, BillFilter filter)
    {
        IQueryable<Bill> query = context.Bills
            .AsNoTracking()
            .Include(navigationPropertyPath: bill => bill.Category)
            .Include(navigationPropertyPath: bill => bill.Participant)
            .Where(predicate: bill => bill.UserId == userId);

        if (!string.IsNullOrWhiteSpace(value: filter.Search))
        {
            string search = filter.Search.ToLower();
            query = query.Where(predicate: transaction =>
                (transaction.Category != null && transaction.Category.Name.ToLower().Contains(search)) ||
                transaction.Participant.Name.ToLower().Contains(search));
        }

        query = filter.ListOrder switch
        {
            ListOrder.Oldest  => query.OrderBy(keySelector: transaction => transaction.DueDate),
            ListOrder.Az      => query.OrderBy(keySelector: transaction => transaction.Participant.Name),
            ListOrder.Za      => query.OrderByDescending(keySelector: transaction => transaction.Participant.Name),
            ListOrder.Highest => query.OrderByDescending(keySelector: transaction => transaction.Amount),
            ListOrder.Lowest  => query.OrderBy(keySelector: transaction => transaction.Amount),
            _                 => query.OrderByDescending(keySelector: transaction => transaction.DueDate)
        };

        return await CreatePageList<Bill>.Execute(query: query, page: filter.Pagination);
    }

    public async Task<BillDashboard> GetDashboard(Guid userId)
    {
        IQueryable<Bill> query = context.Bills
            .AsNoTracking()
            .Include(navigationPropertyPath: bill => bill.Category)
            .Include(navigationPropertyPath: bill => bill.Participant)
            .Where(predicate: bill => bill.UserId == userId);
        
        decimal total = await query.CountAsync();
        decimal paid = await query.CountAsync(predicate: bill => bill.InstallmentsTotal == bill.InstallmentsPaid);
        decimal upcoming = await query.CountAsync(predicate: bill => bill.InstallmentsTotal != bill.InstallmentsPaid);
        decimal dueSoon = await query.CountAsync(predicate: bill =>
            bill.InstallmentsTotal != bill.InstallmentsPaid &&
            bill.DueDate >= DateTime.Today &&
            bill.DueDate <= DateTime.Today.AddDays(7));
        
        return new BillDashboard
        {
            Total = total,
            Paid = paid,
            Upcoming = upcoming,
            DueSoon = dueSoon
        };
    }
    

    public async Task Add(Bill bill)
    {
        await context.Bills.AddAsync(entity: bill);
    }

    public void Update(Bill bill)
    {
        context.Bills.Update(entity: bill);
    }

    public void Delete(Bill bill)
    {
        context.Bills.Remove(entity: bill);
    }

    public async Task<Bill?> GetById(Guid billId, Guid userId)
    {
        return await context.Bills.AsTracking()
            .FirstOrDefaultAsync(predicate: bill => bill.Id == billId && bill.UserId == userId);
    }
}
