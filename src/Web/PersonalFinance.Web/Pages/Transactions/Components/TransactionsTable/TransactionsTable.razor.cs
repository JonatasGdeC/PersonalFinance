using System.Globalization;
using Microsoft.AspNetCore.Components;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Enums;

namespace PersonalFinance.Web.Pages.Transactions.Components.TransactionsTable;

public partial class TransactionsTable : ComponentBase
{
    [Parameter] public List<TransactionDto> ListTransactions { get; set; } = [];
    
    private static readonly CultureInfo DateCulture = new(name: "en-US");
    
    private static string FormatDate(DateTime date) => date.ToString(format: "dd MMM yyyy", provider: DateCulture);

    private static string FormatAmount(TransactionDto transaction)
    {
        string sign = transaction.Type == TransactionType.Income ? "+" : "-";
        return $"{sign}{Math.Abs(value: transaction.Amount).ToString(format: "C2", provider: DateCulture)}";
    }

    private static string GetInitial(string name) => name.Length > 0 ? name[..1].ToUpperInvariant() : string.Empty;
}