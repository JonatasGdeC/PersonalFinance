using Microsoft.EntityFrameworkCore;
using PersonalFinance.Domain.Repositories;
using PersonalFinance.Domain.Requests;

namespace PersonalFinance.Infrastructure.DataAccess.Utils;

internal static class CreatePageList<T>
{
    internal static async Task<PagedList<T>> Execute(IQueryable<T> query, PageRequest page)
    {
        int totalItems = await query.CountAsync();

        int pageNumber = Math.Max(val1: 1, val2: page.PageNumber);
        int pageSize = Math.Max(val1: 1, val2: page.PageSize);
        int skipItems = (pageNumber - 1) * pageSize;

        List<T> items = await query.Skip(count: skipItems).Take(count: pageSize).ToListAsync();

        return new PagedList<T>
        {
            Items = items,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItems = totalItems
        };
    }
}