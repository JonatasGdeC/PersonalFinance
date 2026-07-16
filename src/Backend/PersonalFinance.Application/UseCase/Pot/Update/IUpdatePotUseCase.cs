using PersonalFinance.Communication.Requests.Pot;

namespace PersonalFinance.Application.UseCase.Pot.Update;

public interface IUpdatePotUseCase
{
    Task Execute(long potId, RegisterPotRequest request);
}