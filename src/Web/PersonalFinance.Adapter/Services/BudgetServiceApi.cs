using PersonalFinance.Adapter.Interfaces;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Requests.Budget;
using PersonalFinance.Communication.Responses.Budget;

namespace PersonalFinance.Adapter.Services;

internal class BudgetServiceApi(HttpClient httpClient) : ApiServiceBase(httpClient: httpClient, baseUri: "Budget"), IBudgetServiceApi
{
    public async Task<BudgetDto> Register(RegisterBudgetRequest request) =>
        await PostAsync<RegisterBudgetRequest, BudgetDto>(request: request);

    public async Task Update(long budgetId, RegisterBudgetRequest request) =>
        await PutAsync(request: request, route: $"/{budgetId}");

    public async Task Delete(long budgetId) =>
        await DeleteAsync(route: $"/{budgetId}");

    public async Task<GetAllBudgetResponse?> GetAll() =>
        await GetAsync<GetAllBudgetResponse>();
}
