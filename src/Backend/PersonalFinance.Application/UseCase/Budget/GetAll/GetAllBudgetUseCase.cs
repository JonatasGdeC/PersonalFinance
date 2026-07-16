using AutoMapper;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Responses.Budget;
using PersonalFinance.Domain.Repositories.Budget;
using PersonalFinance.Domain.Services.LoggedUser;

namespace PersonalFinance.Application.UseCase.Budget.GetAll;
using Domain.Entities;

public class GetAllBudgetUseCase(
    IBudgetReadRepository readRepository,
    ILoggedUser loggedUser,
    IMapper mapper) : IGetAllBudgetUseCase
{
    public async Task<GetAllBudgetResponse> Execute()
    {
        User user = await loggedUser.Get();

        List<Budget> budgets = await readRepository.GetAll(userId: user.Id);

        return new GetAllBudgetResponse
        {
            ListBudgets = mapper.Map<List<BudgetDto>>(source: budgets)
        };
    }
}
