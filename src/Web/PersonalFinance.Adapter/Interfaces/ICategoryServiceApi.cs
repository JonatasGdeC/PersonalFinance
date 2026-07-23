using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Enums;
using PersonalFinance.Communication.Requests.Category;
using PersonalFinance.Communication.Responses.Category;

namespace PersonalFinance.Adapter.Interfaces;

public interface ICategoryServiceApi
{
    Task<CategoryDto> Register(RegisterCategoryRequest request);
    Task Update(Guid categoryId, RegisterCategoryRequest request);
    Task Delete(Guid categoryId);
    Task<GetAllCategoryResponse?> GetAll(TransactionType? transactionType = null);
}
