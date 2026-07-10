using Microsoft.EntityFrameworkCore;
using PersonalFinance.Communication.Enums;
using PersonalFinance.Communication.Requests;
using PersonalFinance.Communication.Requests.Bill;
using PersonalFinance.Domain.Entities;
using PersonalFinance.Domain.Repositories;
using PersonalFinance.Domain.Repositories.Bill;
using PersonalFinance.Infrastructure.DataAccess.Utils;

namespace PersonalFinance.Infrastructure.DataAccess.Repositories;

internal class BillRepository(PersonalFinanceDbContext context) : IBillReadRepository, IBillWriteRepository
{
    public async Task<PagedList<Bill>> GetAll(GetAllBillRequest request, Guid userId, PageRequest page)
    {
        IQueryable<Bill> query = context.Bills
            .AsNoTracking()
            .Include(navigationPropertyPath: bill => bill.Category)
            .Include(navigationPropertyPath: bill => bill.Participant)
            .Where(predicate: bill => bill.UserId == userId);
        
        if (!string.IsNullOrWhiteSpace(value: request.Search))
        {
            string search = request.Search.ToLower();
            query = query.Where(predicate: transaction =>
                (transaction.Category != null && transaction.Category.Name.ToLower().Contains(search)) ||
                transaction.Participant.Name.ToLower().Contains(search));
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

        return await CreatePageList<Bill>.Execute(query: query, page: page);
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

    public async Task<Bill?> GetById(long billId, Guid userId)
    {
        return await context.Bills.AsTracking()
            .FirstOrDefaultAsync(predicate: bill => bill.Id == billId && bill.UserId == userId);
    }
}
