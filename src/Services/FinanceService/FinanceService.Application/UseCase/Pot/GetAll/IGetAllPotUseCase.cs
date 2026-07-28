using PersonalFinance.Communication.Responses.Pot;

namespace PersonalFinance.Application.UseCase.Pot.GetAll;

public interface IGetAllPotUseCase
{
    Task<GetAllPotsResponse> Execute();
}