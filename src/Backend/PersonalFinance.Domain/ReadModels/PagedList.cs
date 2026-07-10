namespace PersonalFinance.Domain.ReadModels;

public record PagedList<T>
{
    public required List<T> Items { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages => PageSize <= 0 ? 0 : (int)Math.Ceiling(a: TotalItems / (double)PageSize);
}
