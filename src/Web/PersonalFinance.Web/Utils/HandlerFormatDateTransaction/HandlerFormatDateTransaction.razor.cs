using System.Globalization;
using Microsoft.AspNetCore.Components;
using PersonalFinance.Communication.Dtos;

namespace PersonalFinance.Web.Utils.HandlerFormatDateTransaction;

public partial class HandlerFormatDateTransaction : ComponentBase
{
    [Parameter] public required TransactionDto Transaction { get; init; }

    private static string FormatDate(DateTime date) => date.ToString(format: "dd MMM yyyy", provider: CultureInfo.CurrentCulture);
}