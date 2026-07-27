using PersonalFinance.Communication.Responses.Pot;
using PersonalFinance.Web.UseState.Pot;

namespace PersonalFinance.Web.Pages.Home.Components.SummaryPots;

public partial class SummaryPots
{
    private bool _isLoading = true;

    private double TotalSaved => PotListState.Value.Pots.Sum(selector: pot => pot.CurrentAmount);

    protected override async Task OnInitializedAsync()
    {
        if (!PotListState.Value.Pots.Any())
        {
            Dispatcher.Dispatch(action: new PotActions.GetAllPotsAction());
            GetAllPotsResponse? response = await PersonalFinanceApi.Pot.GetAll();
            if (response != null)
            {
                Dispatcher.Dispatch(action: new PotActions.GetAllPotsSuccessAction(Pots: response.ListPots));
            }
        }

        _isLoading = false;

        await base.OnInitializedAsync();
    }

    private void NavigateToPots() => NavigationManager.NavigateTo(uri: "/pots");
}
