using FluentValidation.Results;
using PersonalFinance.Communication.Requests.Transaction;
using PersonalFinance.Domain.Repositories;
using PersonalFinance.Domain.Repositories.Category;
using PersonalFinance.Domain.Repositories.Participant;
using PersonalFinance.Domain.Repositories.Transaction;
using PersonalFinance.Domain.Services.LoggedUser;
using PersonalFinance.Exception;
using PersonalFinance.Exception.ExceptionBase;

namespace PersonalFinance.Application.UseCase.Transaction.Update;
using Domain.Entities;
using Domain.Enums;

public class UpdateTransactionUseCase(
    ITransactionWhiteRepository writeRepository,
    ICategoryWriteRepository categoryWriteRepository,
    IParticipantWriteRepository participantWriteRepository,
    ILoggedUser loggedUser,
    IUnitOfWork unitOfWork) : IUpdateTransactionUseCase
{
    public async Task Execute(Guid transaction, RegisterTransactionRequest request)
    {
        await Validate(request: request);

        User user = await loggedUser.Get();

        Transaction? existingTransaction = await writeRepository.GetById(transactionId: transaction, userId: user.Id);
        if (existingTransaction == null)
        {
            throw new NotFoundException(message: ResourceErrorMessages.TRANSACTION_NOT_FOUND);
        }

        Participant participant = await GetParticipant(participantId: request.ParticipantId, userId: user.Id);
        Category? category = await GetCategory(categoryId: request.CategoryId, userId: user.Id);

        existingTransaction.Date = request.Date;
        existingTransaction.Type = (TransactionType)(int)request.Type;
        existingTransaction.Amount = request.Amount;
        existingTransaction.ParticipantId = participant.Id;
        existingTransaction.CategoryId = category?.Id;

        writeRepository.Update(transaction: existingTransaction);
        await unitOfWork.Commit();
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

    private async Task Validate(RegisterTransactionRequest request)
    {
        TransactionValidator validator = new();
        ValidationResult? result = await validator.ValidateAsync(instance: request);

        if (!result.IsValid)
        {
            List<string> errors = result.Errors.Select(selector: error => error.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorsMessages: errors);
        }
    }
}
