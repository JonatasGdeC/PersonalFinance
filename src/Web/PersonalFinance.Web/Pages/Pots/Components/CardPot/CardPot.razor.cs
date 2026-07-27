using Microsoft.AspNetCore.Components;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Web.UseState.Modal;
using PersonalFinance.Web.UseState.Pot;

namespace PersonalFinance.Web.Pages.Pots.Components.CardPot;

public partial class CardPot : ComponentBase
{
    [Parameter] public required PotDto Pot { get; init; }

    private bool _isMenuOpen;

    private double SavedPercentage => Pot.Target <= 0 ? 0 : Math.Min(val1: 100, val2: Pot.CurrentAmount / Pot.Target * 100);

    private void ToggleMenu() => _isMenuOpen = !_isMenuOpen;

    private void CloseMenu() => _isMenuOpen = false;

    private void HandleEditClick()
    {
        _isMenuOpen = false;
        Dispatcher.Dispatch(action: new PotActions.GetPotByIdSuccessAction(Pot: Pot));
        Dispatcher.Dispatch(action: new ModalActions.OpenModalAction(Modal: ModalType.EditPot));
    }

    private void HandleDeleteClick()
    {
        _isMenuOpen = false;
        Dispatcher.Dispatch(action: new PotActions.GetPotByIdSuccessAction(Pot: Pot));
        Dispatcher.Dispatch(action: new ModalActions.OpenModalAction(Modal: ModalType.DeletePot));
    }
}
