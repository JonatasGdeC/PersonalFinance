using AutoMapper;
using FluentValidation.Results;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Requests.Participant;
using PersonalFinance.Communication.Validators;
using FinanceService.Domain.Repositories;
using FinanceService.Domain.Repositories.Participant;
using FinanceService.Domain.Services.LoggedUser;
using PersonalFinance.Exception.ExceptionBase;

namespace PersonalFinance.Application.UseCase.Participant.Register;
using FinanceService.Domain.Entities;

public class RegisterParticipantUseCase(
    IParticipantWriteRepository writeRepository,
    ILoggedUser loggedUser,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRegisterParticipantUseCase
{
    public async Task<ParticipantDto> Execute(RegisterParticipantRequest request)
    {
        await Validate(request: request);

        Guid userId = loggedUser.GetUserId();

        Participant participant = mapper.Map<Participant>(source: request);
        participant.UserId = userId;

        await writeRepository.Add(participant: participant);
        await unitOfWork.Commit();

        return mapper.Map<ParticipantDto>(source: participant);
    }

    private async Task Validate(RegisterParticipantRequest request)
    {
        ParticipantValidator validator = new();
        ValidationResult? result = await validator.ValidateAsync(instance: request);

        if (!result.IsValid)
        {
            List<string> errors = result.Errors.Select(selector: error => error.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorsMessages: errors);
        }
    }
}
