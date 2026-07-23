using AutoMapper;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Requests;
using PersonalFinance.Communication.Responses.Transaction;
using PersonalFinance.Domain.Filters;
using PersonalFinance.Domain.ReadModels;
using PersonalFinance.Domain.Repositories.Category;
using PersonalFinance.Domain.Repositories.Transaction;
using PersonalFinance.Domain.Services.LoggedUser;
using PersonalFinance.Exception;
using PersonalFinance.Exception.ExceptionBase;

namespace PersonalFinance.Application.UseCase.Transaction.GetByCategory;
using Domain.Entities;

public class GetTransactionByCategoryIdUseCase(
    ITransactionReadRepository readRepository,
    ICategoryWriteRepository categoryWriteRepository,
    ILoggedUser loggedUser,
    IMapper mapper) : IGetTransactionByCategoryIdUseCase
{
    public async Task<GetListTransactionsResponse> Execute(long categoryId, DateTime date, PaginationRequest pagination)
    {
        User user = await loggedUser.Get();

        Category? category = await categoryWriteRepository.GetById(categoryId: categoryId, userId: user.Id);
        if (category == null)
        {
            throw new NotFoundException(message: ResourceErrorMessages.CATEGORY_NOT_FOUND);
        }

        Pagination domainPagination = new()
        {
            PageNumber = pagination.PageNumber,
            PageSize = pagination.PageSize
        };

        PagedList<Transaction> paged = await readRepository.GetByCategory(
            userId: user.Id,
            categoryId: categoryId,
            date: date,
            pagination: domainPagination);
        
        double totalAmount = await readRepository.GetTotalAmountByCategory(userId: user.Id, categoryId: categoryId, date: date);

        return new GetListTransactionsResponse
        {
            ListTransactions = mapper.Map<List<TransactionDto>>(source: paged.Items),
            PageNumber = paged.PageNumber,
            PageSize = paged.PageSize,
            TotalItems = paged.TotalItems,
            TotalAmount = totalAmount
        };
    }
}
