using System.Globalization;
using Microsoft.AspNetCore.Components;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Web.Resources.Bills;

namespace PersonalFinance.Web.Utils.HandlerBillDueDate;

public partial class HandlerBillDueDate : ComponentBase
{
    [Parameter] public required BillDto Bill { get; init; }

    private bool IsPaid => BillStatusHelper.IsPaid(bill: Bill);
    private bool IsDueSoon => BillStatusHelper.IsDueSoon(bill: Bill);

    private string DueDateText => string.Format(format: BillsResources.MonthlyDueDateFormat, arg0: GetOrdinalDay(day: Bill.DueDate.Day));

    private static string GetOrdinalDay(int day)
    {
        if (CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "pt")
        {
            return day.ToString();
        }

        string suffix = (day % 100) switch
        {
            11 or 12 or 13 => "th",
            _ => (day % 10) switch
            {
                1 => "st",
                2 => "nd",
                3 => "rd",
                _ => "th"
            }
        };

        return $"{day}{suffix}";
    }
}
