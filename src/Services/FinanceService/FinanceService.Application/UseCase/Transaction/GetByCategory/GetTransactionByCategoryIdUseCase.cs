using AutoMapper;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Requests;
using PersonalFinance.Communication.Responses.Transaction;
using FinanceService.Domain.Filters;
using FinanceService.Domain.ReadModels;
using FinanceService.Domain.Repositories.Category;
using FinanceService.Domain.Repositories.Transaction;
using FinanceService.Domain.Services.LoggedUser;
using PersonalFinance.Exception;
using PersonalFinance.Exception.ExceptionBase;

namespace PersonalFinance.Application.UseCase.Transaction.GetByCategory;
using FinanceService.Domain.Entities;

public class GetTransactionByCategoryIdUseCase(
    ITransactionReadRepository readRepository,
    ICategoryWriteRepository categoryWriteRepository,
    ILoggedUser loggedUser,
    IMapper mapper) : IGetTransactionByCategoryIdUseCase
{
    public async Task<GetListTransactionsResponse> Execute(Guid categoryId, DateTime date, PaginationRequest pagination)
    {
        Guid userId = loggedUser.GetUserId();

        Category? category = await categoryWriteRepository.GetById(categoryId: categoryId, userId: userId);
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
            userId: userId,
            categoryId: categoryId,
            date: date,
            pagination: domainPagination);

        double totalAmount = await readRepository.GetTotalAmountByCategory(userId: userId, categoryId: categoryId, date: date);

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
