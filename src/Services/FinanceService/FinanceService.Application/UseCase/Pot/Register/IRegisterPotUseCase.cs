using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Requests.Pot;

namespace PersonalFinance.Application.UseCase.Pot.Register;

public interface IRegisterPotUseCase
{
    Task<PotDto> Execute(RegisterPotRequest request);
}