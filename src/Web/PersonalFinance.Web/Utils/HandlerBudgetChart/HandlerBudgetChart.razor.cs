using ApexCharts;
using Microsoft.AspNetCore.Components;
using PersonalFinance.Communication.Dtos;

namespace PersonalFinance.Web.Utils.HandlerBudgetChart;

public partial class HandlerBudgetChart : ComponentBase
{
    [Parameter] public List<BudgetDto> ListBudgets { get; init; } = [];

    private static readonly ApexChartOptions<BudgetDto> ChartOptions = new()
    {
        DataLabels = new DataLabels { Enabled = false },
        Legend = new Legend { Show = false }
    };
}