namespace PersonalFinance.Application.UseCase.Pot.Delete;

public interface IDeletePotUseCase
{
    Task Execute(long potId);
}