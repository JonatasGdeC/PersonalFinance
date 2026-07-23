using AutoMapper;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Enums;
using PersonalFinance.Communication.Responses.Category;
using PersonalFinance.Domain.Repositories.Category;
using PersonalFinance.Domain.Services.LoggedUser;

namespace PersonalFinance.Application.UseCase.Category.GetAll;
using Domain.Entities;

public class GetAllCategoryUseCase(
    ICategoryReadRepository readRepository,
    ILoggedUser loggedUser,
    IMapper mapper) : IGetAllCategoryUseCase
{
    public async Task<GetAllCategoryResponse> Execute(TransactionType? transactionType = null)
    {
        User user = await loggedUser.Get();

        List<Category> categories = await readRepository.GetAll(userId: user.Id, transactionType: transactionType.HasValue ? (Domain.Enums.TransactionType)transactionType : null);

        return new GetAllCategoryResponse
        {
            ListCategories = mapper.Map<List<CategoryDto>>(source: categories)
        };
    }
}
