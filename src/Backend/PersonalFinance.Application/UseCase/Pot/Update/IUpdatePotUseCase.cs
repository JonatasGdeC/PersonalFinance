using PersonalFinance.Communication.Requests.Pot;

namespace PersonalFinance.Application.UseCase.Pot.Update;

public interface IUpdatePotUseCase
{
    Task Execute(Guid potId, RegisterPotRequest request);
}