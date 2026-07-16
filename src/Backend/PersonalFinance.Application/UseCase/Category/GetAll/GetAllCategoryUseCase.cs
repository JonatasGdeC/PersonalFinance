using AutoMapper;
using PersonalFinance.Communication.Dtos;
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
    public async Task<GetAllCategoryResponse> Execute()
    {
        User user = await loggedUser.Get();

        List<Category> categories = await readRepository.GetAll(userId: user.Id);

        return new GetAllCategoryResponse
        {
            ListCategories = mapper.Map<List<CategoryDto>>(source: categories)
        };
    }
}
