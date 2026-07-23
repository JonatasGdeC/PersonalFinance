using Microsoft.AspNetCore.Components;
using PersonalFinance.Communication.Dtos;

namespace PersonalFinance.Web.Pages.Budgets.Components.BudgetChart;

public partial class BudgetChart : ComponentBase
{
    [Parameter] public List<BudgetDto> ListBudgets { get; init; } = [];
    [Parameter] public bool IsLoading { get; init; }
}