using Fluxor;
using PersonalFinance.Web.UseState.Modal.State;

namespace PersonalFinance.Web.UseState.Modal.Reducers;

using static ModalActions;

public class ReducerCloseModal
{
    [ReducerMethod]
    public static ModalState ReduceCloseModal(ModalState state, CloseModalAction action)
        => new() { OpenModals = state.OpenModals.Where(predicate: modal => modal != action.Modal).ToHashSet() };
}
