using PersonalFinance.Adapter.Interfaces;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Requests.Category;
using PersonalFinance.Communication.Responses.Category;

namespace PersonalFinance.Adapter.Services;

internal class CategoryServiceApi(HttpClient httpClient) : ApiServiceBase(httpClient: httpClient, baseUri: "Category"), ICategoryServiceApi
{
    public async Task<CategoryDto> Register(RegisterCategoryRequest request) =>
        await PostAsync<RegisterCategoryRequest, CategoryDto>(request: request);

    public async Task Update(long categoryId, RegisterCategoryRequest request) =>
        await PutAsync(request: request, route: $"/{categoryId}");

    public async Task Delete(long categoryId) =>
        await DeleteAsync(route: $"/{categoryId}");

    public async Task<GetAllCategoryResponse?> GetAll() =>
        await GetAsync<GetAllCategoryResponse>();
}
