using FluentValidation.Results;
using PersonalFinance.Communication.Requests.Pot;
using PersonalFinance.Communication.Validators;
using FinanceService.Domain.Repositories;
using FinanceService.Domain.Repositories.Pot;
using FinanceService.Domain.Services.LoggedUser;
using PersonalFinance.Exception;
using PersonalFinance.Exception.ExceptionBase;

namespace PersonalFinance.Application.UseCase.Pot.Update;
using FinanceService.Domain.Entities;

public class UpdatePotUseCase(
    IPotWriteRepository writeRepository,
    ILoggedUser loggedUser,
    IUnitOfWork unitOfWork) : IUpdatePotUseCase
{
    public async Task Execute(Guid potId, RegisterPotRequest request)
    {
        await Validate(request: request);

        Guid userId = loggedUser.GetUserId();

        Pot? pot = await writeRepository.GetById(potId: potId, userId: userId);
        if (pot == null)
        {
            throw new NotFoundException(message: ResourceErrorMessages.POT_NOT_FOUND);
        }

        pot.Name = request.Name;
        pot.Color = request.Color;
        pot.Target = request.Target;
        pot.CurrentAmount = request.CurrentAmount;

        writeRepository.Update(pot: pot);
        await unitOfWork.Commit();
    }

    private async Task Validate(RegisterPotRequest request)
    {
        PotValidator validator = new();
        ValidationResult? result = await validator.ValidateAsync(instance: request);

        if (!result.IsValid)
        {
            List<string> errors = result.Errors.Select(selector: error => error.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorsMessages: errors);
        }
    }
}
