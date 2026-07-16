using PersonalFinance.Communication.Requests.Category;

namespace PersonalFinance.Application.UseCase.Category.Update;

public interface IUpdateCategoryUseCase
{
    Task Execute(long categoryId, RegisterCategoryRequest request);
}