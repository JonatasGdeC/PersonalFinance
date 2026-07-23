using PersonalFinance.Adapter.Interfaces;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Enums;
using PersonalFinance.Communication.Requests.Category;
using PersonalFinance.Communication.Responses.Category;

namespace PersonalFinance.Adapter.Services;

internal class CategoryServiceApi(HttpClient httpClient) : ApiServiceBase(httpClient: httpClient, baseUri: "Category"), ICategoryServiceApi
{
    public async Task<CategoryDto> Register(RegisterCategoryRequest request) =>
        await PostAsync<RegisterCategoryRequest, CategoryDto>(request: request);

    public async Task Update(Guid categoryId, RegisterCategoryRequest request) =>
        await PutAsync(request: request, route: $"/{categoryId}");

    public async Task Delete(Guid categoryId) =>
        await DeleteAsync(route: $"/{categoryId}");

    public async Task<GetAllCategoryResponse?> GetAll(TransactionType? transactionType = null)
    {
        string query = BuildQueryString(("TransactionType", transactionType));

        return await GetAsync<GetAllCategoryResponse>(route: query);
    }
}
