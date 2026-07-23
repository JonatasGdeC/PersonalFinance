using PersonalFinance.Communication.Enums;

namespace PersonalFinance.Communication.Requests.Category;

public record RegisterCategoryRequest
{
    public required string Name { get; set; }
    public required TransactionType Type  { get; set; }
}