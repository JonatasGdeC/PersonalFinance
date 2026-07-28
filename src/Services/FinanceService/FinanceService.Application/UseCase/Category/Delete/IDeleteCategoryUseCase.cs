namespace PersonalFinance.Application.UseCase.Category.Delete;

public interface IDeleteCategoryUseCase
{
    Task Execute(Guid categoryId);
}