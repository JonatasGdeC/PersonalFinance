using AutoMapper;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Requests.Transaction;
using PersonalFinance.Communication.Responses.Transaction;
using PersonalFinance.Domain.Filters;
using PersonalFinance.Domain.ReadModels;
using PersonalFinance.Domain.Repositories.Transaction;
using PersonalFinance.Domain.Services.LoggedUser;

namespace PersonalFinance.Application.UseCase.Transaction.GetAll;
using Domain.Entities;
using Domain.Enums;

public class GetAllTransactionUseCase(
    ITransactionReadRepository readRepository,
    ILoggedUser loggedUser,
    IMapper mapper) : IGetAllTransactionUseCase
{
    public async Task<GetListTransactionsResponse> Execute(TransactionFilterRequest request)
    {
        User user = await loggedUser.Get();

        TransactionFilter filter = new()
        {
            Search = request.Search,
            ListOrder = (ListOrder)(int)request.ListOrder,
            CategoryId = request.CategoryId,
            TransactionType = request.TransactionType.HasValue ? (TransactionType)(int)request.TransactionType.Value : null,
            Pagination = new Pagination
            {
                PageNumber = request.Pagination.PageNumber,
                PageSize = request.Pagination.PageSize
            }
        };

        PagedList<Transaction> paged = await readRepository.GetAll(userId: user.Id, filter: filter);

        return new GetListTransactionsResponse
        {
            ListTransactions = mapper.Map<List<TransactionDto>>(source: paged.Items),
            PageNumber = paged.PageNumber,
            PageSize = paged.PageSize,
            TotalItems = paged.TotalItems
        };
    }
}
