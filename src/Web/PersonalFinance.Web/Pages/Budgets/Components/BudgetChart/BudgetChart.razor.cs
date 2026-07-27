using Microsoft.AspNetCore.Components;
using PersonalFinance.Communication.Dtos;

namespace PersonalFinance.Web.Pages.Budgets.Components.BudgetChart;

public partial class BudgetChart
{
    [Parameter] public bool IsLoading { get; init; }
}