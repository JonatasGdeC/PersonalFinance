using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Requests.Transaction;

namespace PersonalFinance.Application.UseCase.Transaction.Register;

public interface IRegisterTransactionUseCase
{
    Task<TransactionDto> Execute(RegisterTransactionRequest request);
}