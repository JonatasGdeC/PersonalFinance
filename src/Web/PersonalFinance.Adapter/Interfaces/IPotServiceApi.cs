using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Requests.Pot;
using PersonalFinance.Communication.Responses.Pot;

namespace PersonalFinance.Adapter.Interfaces;

public interface IPotServiceApi
{
    Task<PotDto> Register(RegisterPotRequest request);
    Task Update(long potId, RegisterPotRequest request);
    Task Delete(long potId);
    Task<GetAllPotsResponse?> GetAll();
}
