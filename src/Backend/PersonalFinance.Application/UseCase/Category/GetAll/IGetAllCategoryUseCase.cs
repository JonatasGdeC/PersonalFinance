using PersonalFinance.Communication.Responses.Category;

namespace PersonalFinance.Application.UseCase.Category.GetAll;

public interface IGetAllCategoryUseCase
{
    Task<GetAllCategoryResponse> Execute();
}