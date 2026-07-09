namespace PersonalFinance.Domain.Repositories.Category;
using Entities;

public interface ICategoryReadRepository
{
    Task<List<Category>> GetAll(Guid userId);
}