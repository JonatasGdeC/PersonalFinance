namespace FinanceService.Domain.Repositories;

public interface IUnitOfWork
{
    Task Commit();
}