using Microsoft.AspNetCore.Components;
using PersonalFinance.Communication.Dtos;

namespace PersonalFinance.Web.Pages.Transactions.Components.TransactionsTable;

public partial class TransactionsTable : ComponentBase
{
    [Parameter] public List<TransactionDto> ListTransactions { get; set; } = [];
}