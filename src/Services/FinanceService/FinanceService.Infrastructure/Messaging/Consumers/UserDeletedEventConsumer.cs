using FinanceService.Domain.Repositories;
using FinanceService.Domain.Repositories.Bill;
using FinanceService.Domain.Repositories.Budget;
using FinanceService.Domain.Repositories.Category;
using FinanceService.Domain.Repositories.Participant;
using FinanceService.Domain.Repositories.Pot;
using FinanceService.Domain.Repositories.Transaction;
using MassTransit;
using PersonalFinance.Contracts.Events;

namespace FinanceService.Infrastructure.Messaging.Consumers;

public class UserDeletedEventConsumer(
    IBillWriteRepository billWriteRepository,
    IBudgetWriteRepository budgetWriteRepository,
    ICategoryWriteRepository categoryWriteRepository,
    IParticipantWriteRepository participantWriteRepository,
    IPotWriteRepository potWriteRepository,
    ITransactionWhiteRepository transactionWhiteRepository,
    IUnitOfWork unitOfWork) : IConsumer<UserDeletedEvent>
{
    public async Task Consume(ConsumeContext<UserDeletedEvent> context)
    {
        Guid userId = context.Message.UserId;

        await billWriteRepository.DeleteByUserId(userId: userId);
        await budgetWriteRepository.DeleteByUserId(userId: userId);
        await categoryWriteRepository.DeleteByUserId(userId: userId);
        await participantWriteRepository.DeleteByUserId(userId: userId);
        await potWriteRepository.DeleteByUserId(userId: userId);
        await transactionWhiteRepository.DeleteByUserId(userId: userId);
        
        await unitOfWork.Commit();
    }
}
