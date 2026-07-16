namespace PersonalFinance.Communication.Responses;

public record PaginationResponse
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
    public int TotalItems { get; init; }
    public int TotalPages => PageSize <= 0 ? 0 : (int)Math.Ceiling(a: TotalItems / (double)PageSize);
}