using Microsoft.EntityFrameworkCore;
using PersonalFinance.Domain.Entities;
using PersonalFinance.Domain.Repositories.Category;

namespace PersonalFinance.Infrastructure.DataAccess.Repositories;

internal class CategoryRepository(PersonalFinanceDbContext context) : ICategoryReadRepository, ICategoryWriteRepository
{
    public async Task<List<Category>> GetAll(Guid userId) =>
        await context.Categories.AsNoTracking().Where(predicate: category => category.UserId == userId).ToListAsync();

    public async Task Add(Category category)
    {
        await context.Categories.AddAsync(entity: category);
    }

    public void Update(Category category)
    {
        context.Categories.Update(entity: category);
    }

    public void Delete(Category category)
    {
        context.Categories.Remove(entity: category);
    }

    public async Task<Category?> GetById(long categoryId, Guid userId)
    {
        return await context.Categories.AsTracking()
            .FirstOrDefaultAsync(predicate: category => category.Id == categoryId && userId == category.UserId);
    }
}