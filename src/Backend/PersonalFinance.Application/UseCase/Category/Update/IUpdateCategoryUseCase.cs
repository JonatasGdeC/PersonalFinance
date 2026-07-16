using PersonalFinance.Communication.Requests.Category;

namespace PersonalFinance.Application.UseCase.Category.Update;

public interface IUpdateCategoryUseCase
{
    Task Execute(long potId, RegisterCategoryRequest request);
}