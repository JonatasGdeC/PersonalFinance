using Microsoft.EntityFrameworkCore;
using PersonalFinance.Domain.Entities;
using PersonalFinance.Domain.Repositories.Category;

namespace PersonalFinance.Infrastructure.DataAccess.Repositories;

internal class CategoryRepository(PersonalFinanceDbContext context) : ICategoryReadRepository, ICategoryWriteRepository
{
    public async Task<List<Category>> GetAll(Guid userId) => await context.Categorys.AsNoTracking().ToListAsync();

    public async Task Add(Category category)
    {
        await context.Categorys.AddAsync(entity: category);
    }

    public void Update(Category category)
    {
        context.Categorys.Update(entity: category);
    }

    public void Delete(Category category)
    {
        context.Categorys.Remove(entity: category);
    }

    public async Task<Category?> GetById(long categoryId, Guid userId)
    {
        return await context.Categorys.AsTracking()
            .FirstOrDefaultAsync(predicate: category => category.Id == categoryId && userId == category.UserId);
    }
}