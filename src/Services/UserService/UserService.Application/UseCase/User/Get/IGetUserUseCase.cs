using PersonalFinance.Communication.Dtos;

namespace UserService.Application.UseCase.User.Get;

public interface IGetUserUseCase
{
    Task<UserDto> Execute();
}