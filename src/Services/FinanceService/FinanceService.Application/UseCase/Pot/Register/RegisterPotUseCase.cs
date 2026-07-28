using AutoMapper;
using FluentValidation.Results;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Requests.Pot;
using PersonalFinance.Communication.Validators;
using FinanceService.Domain.Repositories;
using FinanceService.Domain.Repositories.Pot;
using FinanceService.Domain.Services.LoggedUser;
using PersonalFinance.Exception.ExceptionBase;

namespace PersonalFinance.Application.UseCase.Pot.Register;
using FinanceService.Domain.Entities;

public class RegisterPotUseCase(
    IPotWriteRepository writeRepository,
    ILoggedUser loggedUser,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRegisterPotUseCase
{
    public async Task<PotDto> Execute(RegisterPotRequest request)
    {
        await Validate(request: request);

        User user = await loggedUser.Get();

        Pot pot = mapper.Map<Pot>(source: request);
        pot.UserId = user.Id;

        await writeRepository.Add(pot: pot);
        await unitOfWork.Commit();

        return mapper.Map<PotDto>(source: pot);
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
