using FinanceService.Domain.Repositories;
using FinanceService.Domain.Repositories.Category;
using FinanceService.Domain.Services.LoggedUser;
using PersonalFinance.Exception;
using PersonalFinance.Exception.ExceptionBase;

namespace PersonalFinance.Application.UseCase.Category.Delete;
using FinanceService.Domain.Entities;

public class DeleteCategoryUseCase(
    ICategoryWriteRepository writeRepository,
    ILoggedUser loggedUser,
    IUnitOfWork unitOfWork) : IDeleteCategoryUseCase
{
    public async Task Execute(Guid categoryId)
    {
        Guid userId = loggedUser.GetUserId();

        Category? category = await writeRepository.GetById(categoryId: categoryId, userId: userId);
        if (category == null)
        {
            throw new NotFoundException(message: ResourceErrorMessages.CATEGORY_NOT_FOUND);
        }

        writeRepository.Delete(category: category);
        await unitOfWork.Commit();
    }
}
