using PersonalFinance.Adapter.Interfaces;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Requests.Bill;
using PersonalFinance.Communication.Responses.Bill;

namespace PersonalFinance.Adapter.Services;

internal class BillServiceApi(HttpClient httpClient) : ApiServiceBase(httpClient: httpClient, baseUri: "Bill"), IBillServiceApi
{
    public async Task<BillDto> Register(RegisterBillRequest request) =>
        await PostAsync<RegisterBillRequest, BillDto>(request: request);

    public async Task Update(long billId, RegisterBillRequest request) =>
        await PutAsync(request: request, route: $"/{billId}");

    public async Task Delete(long billId) =>
        await DeleteAsync(route: $"/{billId}");

    public async Task<GetAllBillResponse?> GetAll(BillFilterRequest filter)
    {
        string query = BuildQueryString(
            ("Search", filter.Search),
            ("ListOrder", filter.ListOrder),
            ("Pagination.PageNumber", filter.Pagination.PageNumber),
            ("Pagination.PageSize", filter.Pagination.PageSize));

        return await GetAsync<GetAllBillResponse>(route: query);
    }

    public async Task<GetBillDashboardResponse?> GetDashboard() =>
        await GetAsync<GetBillDashboardResponse>(route: "/dashboard");
}
