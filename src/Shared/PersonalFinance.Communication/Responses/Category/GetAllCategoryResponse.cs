using PersonalFinance.Communication.Dtos;

namespace PersonalFinance.Communication.Responses.Category;

public record GetAllCategoryResponse
{
    public List<CategoryDto> ListCategories { get; init; } = [];
}