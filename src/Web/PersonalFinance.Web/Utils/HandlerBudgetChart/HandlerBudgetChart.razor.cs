using ApexCharts;
using PersonalFinance.Communication.Dtos;

namespace PersonalFinance.Web.Utils.HandlerBudgetChart;

public partial class HandlerBudgetChart
{
    private static readonly ApexChartOptions<BudgetDto> ChartOptions = new()
    {
        DataLabels = new DataLabels { Enabled = false },
        Legend = new Legend { Show = false }
    };
}