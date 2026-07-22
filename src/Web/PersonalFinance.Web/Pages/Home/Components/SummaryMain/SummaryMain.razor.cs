using System.Globalization;
using Microsoft.AspNetCore.Components;

namespace PersonalFinance.Web.Pages.Home.Components.SummaryMain;

public partial class SummaryMain : ComponentBase
{
    [Parameter] public bool IsLoading { get; init; }
    [Parameter] public double? CurrentBalance { get; init; }
    [Parameter] public double? TotalIncome { get; init; }
    [Parameter] public double? TotalExpense { get; init; }

    private string FormattedCurrentBalance => FormatCurrency(value: CurrentBalance);
    private string FormattedTotalIncome => FormatCurrency(value: TotalIncome);
    private string FormattedTotalExpense => FormatCurrency(value: TotalExpense);

    private static string FormatCurrency(double? value) => (value ?? 0).ToString(format: "C2", provider: CultureInfo.CurrentCulture);
}