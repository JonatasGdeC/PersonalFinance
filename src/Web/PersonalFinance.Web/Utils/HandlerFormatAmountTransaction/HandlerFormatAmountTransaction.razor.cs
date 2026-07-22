using System.Globalization;
using Microsoft.AspNetCore.Components;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Enums;

namespace PersonalFinance.Web.Utils.HandlerFormatAmountTransaction;

public partial class HandlerFormatAmountTransaction : ComponentBase
{
    [Parameter] public required TransactionDto Transaction { get; init; }
    
    private static string FormatAmount(TransactionDto transaction)
    {
        string sign = transaction.Type == TransactionType.Income ? "+" : "-";

        return $"{sign}{Math.Abs(value: transaction.Amount).ToString(format: "C2", provider: CultureInfo.CurrentCulture)}";
    }
}