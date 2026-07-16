using PersonalFinance.Adapter.Interfaces;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Requests.Participant;
using PersonalFinance.Communication.Responses.Participant;

namespace PersonalFinance.Adapter.Services;

internal class ParticipantServiceApi(HttpClient httpClient) : ApiServiceBase(httpClient: httpClient, baseUri: "Participant"), IParticipantServiceApi
{
    public async Task<ParticipantDto> Register(RegisterParticipantRequest request) =>
        await PostAsync<RegisterParticipantRequest, ParticipantDto>(request: request);

    public async Task Update(long participantId, RegisterParticipantRequest request) =>
        await PutAsync(request: request, route: $"/{participantId}");

    public async Task Delete(long participantId) =>
        await DeleteAsync(route: $"/{participantId}");

    public async Task<GetAllParticipantResponse?> GetAll() =>
        await GetAsync<GetAllParticipantResponse>();

    public async Task<ParticipantDto?> GetById(long participantId) =>
        await GetAsync<ParticipantDto>(route: $"/{participantId}");
}
