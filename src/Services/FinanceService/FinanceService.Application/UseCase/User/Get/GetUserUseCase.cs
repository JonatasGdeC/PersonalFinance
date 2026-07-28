using AutoMapper;
using PersonalFinance.Communication.Dtos;
using FinanceService.Domain.Services.LoggedUser;

namespace PersonalFinance.Application.UseCase.User.Get;
using FinanceService.Domain.Entities;

public class GetUserUseCase(ILoggedUser loggedUser, IMapper mapper) : IGetUserUseCase
{
    public async Task<UserDto> Execute()
    {
        User user = await loggedUser.Get();
        return mapper.Map<UserDto>(source: user);
    }
}
