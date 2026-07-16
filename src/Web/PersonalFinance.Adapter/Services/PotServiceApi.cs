using PersonalFinance.Adapter.Interfaces;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Requests.Pot;
using PersonalFinance.Communication.Responses.Pot;

namespace PersonalFinance.Adapter.Services;

internal class PotServiceApi(HttpClient httpClient) : ApiServiceBase(httpClient: httpClient, baseUri: "Pot"), IPotServiceApi
{
    public async Task<PotDto> Register(RegisterPotRequest request) =>
        await PostAsync<RegisterPotRequest, PotDto>(request: request);

    public async Task Update(long potId, RegisterPotRequest request) =>
        await PutAsync(request: request, route: $"/{potId}");

    public async Task Delete(long potId) =>
        await DeleteAsync(route: $"/{potId}");

    public async Task<GetAllPotsResponse?> GetAll() =>
        await GetAsync<GetAllPotsResponse>();
}
