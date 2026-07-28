using AutoMapper;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Responses.Pot;
using FinanceService.Domain.Repositories.Pot;
using FinanceService.Domain.Services.LoggedUser;

namespace PersonalFinance.Application.UseCase.Pot.GetAll;
using FinanceService.Domain.Entities;

public class GetAllPotUseCase(
    IPotReadRepository readRepository,
    ILoggedUser loggedUser,
    IMapper mapper) : IGetAllPotUseCase
{
    public async Task<GetAllPotsResponse> Execute()
    {
        Guid userId = loggedUser.GetUserId();

        List<Pot> pots = await readRepository.GetAll(userId: userId);

        return new GetAllPotsResponse
        {
            ListPots = mapper.Map<List<PotDto>>(source: pots)
        };
    }
}
