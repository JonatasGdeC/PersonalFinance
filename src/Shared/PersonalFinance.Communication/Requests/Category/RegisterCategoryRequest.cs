namespace PersonalFinance.Communication.Requests.Category;

public record RegisterCategoryRequest
{
    public required string Name { get; set; }
}