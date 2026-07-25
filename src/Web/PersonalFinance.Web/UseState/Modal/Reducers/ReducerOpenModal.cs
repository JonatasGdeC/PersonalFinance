using Fluxor;
using PersonalFinance.Web.UseState.Modal.State;

namespace PersonalFinance.Web.UseState.Modal.Reducers;

using static ModalActions;

public class ReducerOpenModal
{
    [ReducerMethod]
    public static ModalState ReduceOpenModal(ModalState state, OpenModalAction action)
        => new() { OpenModals = [..state.OpenModals, action.Modal] };
}
