using AutoMapper;
using FluentValidation.Results;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Requests.Bill;
using PersonalFinance.Domain.Repositories;
using PersonalFinance.Domain.Repositories.Bill;
using PersonalFinance.Domain.Repositories.Category;
using PersonalFinance.Domain.Repositories.Participant;
using PersonalFinance.Domain.Services.LoggedUser;
using PersonalFinance.Exception;
using PersonalFinance.Exception.ExceptionBase;

namespace PersonalFinance.Application.UseCase.Bill.Register;
using Domain.Entities;

public class RegisterBillUseCase(
    IBillWriteRepository writeRepository,
    ICategoryWriteRepository categoryWriteRepository,
    IParticipantWriteRepository participantWriteRepository,
    ILoggedUser loggedUser,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRegisterBillUseCase
{
    public async Task<BillDto> Execute(RegisterBillRequest request)
    {
        await Validate(request: request);

        User user = await loggedUser.Get();

        Participant participant = await GetParticipant(participantId: request.ParticipantId, userId: user.Id);
        Category? category = await GetCategory(categoryId: request.CategoryId, userId: user.Id);

        Bill bill = mapper.Map<Bill>(source: request);
        bill.UserId = user.Id;
        bill.Participant = participant;
        bill.Category = category;

        await writeRepository.Add(bill: bill);
        await unitOfWork.Commit();

        return mapper.Map<BillDto>(source: bill);
    }

    private async Task<Participant> GetParticipant(Guid participantId, Guid userId)
    {
        Participant? participant = await participantWriteRepository.GetById(participantId: participantId, userId: userId);
        if (participant == null)
        {
            throw new NotFoundException(message: ResourceErrorMessages.PARTICIPANT_NOT_FOUND);
        }

        return participant;
    }

    private async Task<Category?> GetCategory(Guid? categoryId, Guid userId)
    {
        if (!categoryId.HasValue)
        {
            return null;
        }

        Category? category = await categoryWriteRepository.GetById(categoryId: categoryId.Value, userId: userId);
        if (category == null)
        {
            throw new NotFoundException(message: ResourceErrorMessages.CATEGORY_NOT_FOUND);
        }

        return category;
    }

    private async Task Validate(RegisterBillRequest request)
    {
        BillValidator validator = new();
        ValidationResult? result = await validator.ValidateAsync(instance: request);

        if (!result.IsValid)
        {
            List<string> errors = result.Errors.Select(selector: error => error.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorsMessages: errors);
        }
    }
}
