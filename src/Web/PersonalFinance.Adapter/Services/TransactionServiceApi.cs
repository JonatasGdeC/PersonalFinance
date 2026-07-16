using PersonalFinance.Adapter.Interfaces;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Requests;
using PersonalFinance.Communication.Requests.Transaction;
using PersonalFinance.Communication.Responses.Transaction;

namespace PersonalFinance.Adapter.Services;

internal class TransactionServiceApi(HttpClient httpClient) : ApiServiceBase(httpClient: httpClient, baseUri: "Transaction"), ITransactionServiceApi
{
    public async Task<TransactionDto> Register(RegisterTransactionRequest request) =>
        await PostAsync<RegisterTransactionRequest, TransactionDto>(request: request);

    public async Task Update(long transactionId, RegisterTransactionRequest request) =>
        await PutAsync(request: request, route: $"/{transactionId}");

    public async Task Delete(long transactionId) =>
        await DeleteAsync(route: $"/{transactionId}");

    public async Task<GetListTransactionsResponse?> GetAll(TransactionFilterRequest request)
    {
        string query = BuildQueryString(
            ("Search", request.Search),
            ("ListOrder", request.ListOrder),
            ("CategoryId", request.CategoryId),
            ("TransactionType", request.TransactionType),
            ("Pagination.PageNumber", request.Pagination.PageNumber),
            ("Pagination.PageSize", request.Pagination.PageSize));

        return await GetAsync<GetListTransactionsResponse>(route: query);
    }

    public async Task<GetTransactionDashboardResponse?> GetDashboard(DateTime date)
    {
        string query = BuildQueryString(("date", date));

        return await GetAsync<GetTransactionDashboardResponse>(route: $"/dashboard{query}");
    }

    public async Task<GetListTransactionsResponse?> GetByCategory(long categoryId, DateTime date, PaginationRequest pagination)
    {
        string query = BuildQueryString(
            ("date", date),
            ("PageNumber", pagination.PageNumber),
            ("PageSize", pagination.PageSize));

        return await GetAsync<GetListTransactionsResponse>(route: $"/category/{categoryId}{query}");
    }
}
