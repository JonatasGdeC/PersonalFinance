namespace PersonalFinance.Domain.Repositories.Category;
using Entities;

public interface ICategoryWriteRepository
{
    Task Add(Category category);
    void Update(Category category);
    void Delete(Category category);
    Task<Category?> GetById(long categoryId, Guid userId);
}