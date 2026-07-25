using PersonalFinance.Communication.Dtos;

namespace PersonalFinance.Web.UseState.Budget;

public abstract class BudgetActions
{
    public record DeleteBudgetSuccessAction(Guid BudgetId);
    public record GetAllBudgetsAction;
    public record GetAllBudgetsSuccessAction(List<BudgetDto> Budgets);
    public record GetBudgetByIdAction(Guid BudgetId);
    public record GetBudgetByIdSuccessAction(BudgetDto Budget);
    public record RegisterBudgetSuccessAction(BudgetDto Budget);
    public record UpdateBudgetSuccessAction(BudgetDto Budget);
}
