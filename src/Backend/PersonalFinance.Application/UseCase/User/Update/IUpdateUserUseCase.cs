namespace PersonalFinance.Application.UseCase.User.Update;

public interface IUpdateUserUseCase
{
    Task Execute(UpdateUserRequest request);
}
