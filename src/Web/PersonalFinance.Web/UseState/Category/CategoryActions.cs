using PersonalFinance.Communication.Dtos;

namespace PersonalFinance.Web.UseState.Category;

public  abstract class CategoryActions
{
    public abstract record DeleteCategorySuccessAction(Guid CategoryId);
    public record GetAllCategoriesAction;
    public abstract record GetAllCategoriesSuccessAction(List<CategoryDto> Categories);
    public record GetCategoryByIdAction(Guid CategoryId);
    public record GetCategoryByIdSuccessAction(CategoryDto Category);
    public record RegisterCategorySuccessAction(CategoryDto Category);
    public record UpdateCategorySuccessAction(CategoryDto Category);
}