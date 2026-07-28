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
        User user = await loggedUser.Get();

        List<Category> categories = await readRepository.GetAll(userId: user.Id, transactionType: transactionType.HasValue ? (FinanceService.Domain.Enums.TransactionType)transactionType : null);

        return new GetAllCategoryResponse
        {
            ListCategories = mapper.Map<List<CategoryDto>>(source: categories)
        };
    }
}
