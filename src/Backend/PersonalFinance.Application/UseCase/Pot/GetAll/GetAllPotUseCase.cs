using AutoMapper;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Responses.Pot;
using PersonalFinance.Domain.Repositories.Pot;
using PersonalFinance.Domain.Services.LoggedUser;

namespace PersonalFinance.Application.UseCase.Pot.GetAll;
using Domain.Entities;

public class GetAllPotUseCase(
    IPotReadRepository readRepository,
    ILoggedUser loggedUser,
    IMapper mapper) : IGetAllPotUseCase
{
    public async Task<GetAllPotsResponse> Execute()
    {
        User user = await loggedUser.Get();

        List<Pot> pots = await readRepository.GetAll(userId: user.Id);

        return new GetAllPotsResponse
        {
            ListPots = mapper.Map<List<PotDto>>(source: pots)
        };
    }
}
