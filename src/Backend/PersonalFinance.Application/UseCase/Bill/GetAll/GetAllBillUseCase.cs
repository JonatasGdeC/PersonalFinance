using AutoMapper;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Requests.Bill;
using PersonalFinance.Communication.Responses.Bill;
using PersonalFinance.Domain.Filters;
using PersonalFinance.Domain.ReadModels;
using PersonalFinance.Domain.Repositories.Bill;
using PersonalFinance.Domain.Services.LoggedUser;

namespace PersonalFinance.Application.UseCase.Bill.GetAll;
using Domain.Entities;
using Domain.Enums;

public class GetAllBillUseCase(
    IBillReadRepository readRepository,
    ILoggedUser loggedUser,
    IMapper mapper) : IGetAllBillUseCase
{
    public async Task<GetAllBillResponse> Execute(BillFilterRequest filter)
    {
        User user = await loggedUser.Get();

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

        PagedList<Bill> paged = await readRepository.GetAll(userId: user.Id, filter: billFilter);

        return new GetAllBillResponse
        {
            ListBills = mapper.Map<List<BillDto>>(source: paged.Items),
            PageNumber = paged.PageNumber,
            PageSize = paged.PageSize,
            TotalItems = paged.TotalItems
        };
    }
}
