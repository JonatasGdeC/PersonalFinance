using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Requests.Category;

namespace PersonalFinance.Application.UseCase.Category.Register;

public interface IRegisterCategoryUseCase
{
    Task<CategoryDto> Execute(RegisterCategoryRequest request);
}