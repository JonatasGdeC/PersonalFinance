using PersonalFinance.Communication.Requests;
using PersonalFinance.Communication.Responses.Transaction;

namespace PersonalFinance.Application.UseCase.Transaction.GetByCategory;

public interface IGetTransactionByCategoryIdUseCase
{
    Task<GetListTransactionsResponse> Execute(long categoryId, DateTime date, PaginationRequest pagination);
}