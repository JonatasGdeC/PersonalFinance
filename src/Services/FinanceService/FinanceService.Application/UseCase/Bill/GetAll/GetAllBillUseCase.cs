using AutoMapper;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Requests.Bill;
using PersonalFinance.Communication.Responses.Bill;
using FinanceService.Domain.Filters;
using FinanceService.Domain.ReadModels;
using FinanceService.Domain.Repositories.Bill;
using FinanceService.Domain.Services.LoggedUser;

namespace PersonalFinance.Application.UseCase.Bill.GetAll;
using FinanceService.Domain.Entities;
using FinanceService.Domain.Enums;

public class GetAllBillUseCase(
    IBillReadRepository readRepository,
    ILoggedUser loggedUser,
    IMapper mapper) : IGetAllBillUseCase
{
    public async Task<GetAllBillResponse> Execute(BillFilterRequest filter)
    {
        Guid userId = loggedUser.GetUserId();

        BillFilter billFilter = new()
        {
            Search = filter.Search,
            ListOrder = (ListOrder)(int)filter.ListOrder,
            Pagination = new Pagination
            {
                PageNumber = filter.Pagination.PageNumber,
                PageSize = filter.Pagination.PageSize
            }
        };

        PagedList<Bill> paged = await readRepository.GetAll(userId: userId, filter: billFilter);

        return new GetAllBillResponse
        {
            ListBills = mapper.Map<List<BillDto>>(source: paged.Items),
            PageNumber = paged.PageNumber,
            PageSize = paged.PageSize,
            TotalItems = paged.TotalItems
        };
    }
}
