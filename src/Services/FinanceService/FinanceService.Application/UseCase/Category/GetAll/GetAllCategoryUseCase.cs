using AutoMapper;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Enums;
using PersonalFinance.Communication.Responses.Category;
using FinanceService.Domain.Repositories.Category;
using FinanceService.Domain.Services.LoggedUser;

namespace PersonalFinance.Application.UseCase.Category.GetAll;
using FinanceService.Domain.Entities;

public class GetAllCategoryUseCase(
    ICategoryReadRepository readRepository,
    ILoggedUser loggedUser,
    IMapper mapper) : IGetAllCategoryUseCase
{
    public async Task<GetAllCategoryResponse> Execute(FinancialType? transactionType = null)
    {
        Guid userId = loggedUser.GetUserId();

        List<Category> categories = await readRepository.GetAll(userId: userId, transactionType: transactionType.HasValue ? (FinanceService.Domain.Enums.TransactionType)transactionType : null);

        return new GetAllCategoryResponse
        {
            ListCategories = mapper.Map<List<CategoryDto>>(source: categories)
        };
    }
}
