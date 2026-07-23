using PersonalFinance.Communication.Enums;
using PersonalFinance.Communication.Responses.Category;

namespace PersonalFinance.Application.UseCase.Category.GetAll;

public interface IGetAllCategoryUseCase
{
    Task<GetAllCategoryResponse> Execute(TransactionType? transactionType = null);
}