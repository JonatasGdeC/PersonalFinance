using PersonalFinance.Communication.Responses.Pot;
using PersonalFinance.Web.UseState.Modal;
using PersonalFinance.Web.UseState.Pot;

namespace PersonalFinance.Web.Pages.Pots.Main;

public partial class Pots
{
    private bool _isLoading = true;

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

    private void OpenAddPotModal() => Dispatcher.Dispatch(action: new ModalActions.OpenModalAction(Modal: ModalType.AddPot));
}
