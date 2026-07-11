using PersonalFinance.Communication.Dtos;

namespace PersonalFinance.Application.UseCase.User.Get;

public interface IGetUserUseCase
{
    Task<UserDto> Execute();
}