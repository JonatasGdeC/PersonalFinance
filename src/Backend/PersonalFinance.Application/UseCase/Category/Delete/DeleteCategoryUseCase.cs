using PersonalFinance.Domain.Repositories;
using PersonalFinance.Domain.Repositories.Category;
using PersonalFinance.Domain.Services.LoggedUser;
using PersonalFinance.Exception;
using PersonalFinance.Exception.ExceptionBase;

namespace PersonalFinance.Application.UseCase.Category.Delete;
using Domain.Entities;

public class DeleteCategoryUseCase(
    ICategoryWriteRepository writeRepository,
    ILoggedUser loggedUser,
    IUnitOfWork unitOfWork) : IDeleteCategoryUseCase
{
    public async Task Execute(long categoryId)
    {
        User user = await loggedUser.Get();

        Category? category = await writeRepository.GetById(categoryId: categoryId, userId: user.Id);
        if (category == null)
        {
            throw new NotFoundException(message: ResourceErrorMessages.CATEGORY_NOT_FOUND);
        }

        writeRepository.Delete(category: category);
        await unitOfWork.Commit();
    }
}
