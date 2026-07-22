using Microsoft.AspNetCore.Components;
using PersonalFinance.Communication.Dtos;

namespace PersonalFinance.Web.Pages.Home.Components.SummaryTransactions;

public partial class SummaryTransactions : ComponentBase
{
    [Parameter] public bool IsLoading { get; init; }
    [Parameter] public List<TransactionDto> LastestTransactions { get; init; } = [];

    private void NavigateToTransaction() => NavigationManager.NavigateTo(uri: "/transactions");
}