using Microsoft.AspNetCore.Components;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Web.UseState.Budget;
using PersonalFinance.Web.UseState.Modal;

namespace PersonalFinance.Web.Pages.Budgets.Components.CardBudget;

public partial class CardBudget : ComponentBase
{
    [Parameter] public required BudgetDto Budget { get; init; }

    private double _spentAmount;
    private bool _isMenuOpen;

    private double SpentPercentage => Budget.MaximumSpend <= 0 ? 0 : Math.Min(val1: 100, val2: _spentAmount / Budget.MaximumSpend * 100);

    private void HandleSpentLoaded(double spentAmount)
    {
        _spentAmount = spentAmount;
        StateHasChanged();
    }

    private void ToggleMenu() => _isMenuOpen = !_isMenuOpen;

    private void CloseMenu() => _isMenuOpen = false;

    private void HandleEditClick()
    {
        _isMenuOpen = false;
        Dispatcher.Dispatch(action: new BudgetActions.GetBudgetByIdSuccessAction(Budget: Budget));
        Dispatcher.Dispatch(action: new ModalActions.OpenModalAction(Modal: ModalType.EditBudget));
    }

    private void HandleDeleteClick()
    {
        _isMenuOpen = false;
        Dispatcher.Dispatch(action: new BudgetActions.GetBudgetByIdSuccessAction(Budget: Budget));
        Dispatcher.Dispatch(action: new ModalActions.OpenModalAction(Modal: ModalType.DeleteBudget));
    }
}
