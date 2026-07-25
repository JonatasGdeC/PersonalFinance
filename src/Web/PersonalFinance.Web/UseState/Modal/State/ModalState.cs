using Fluxor;

namespace PersonalFinance.Web.UseState.Modal.State;

[FeatureState]
public record ModalState
{
    public HashSet<ModalType> OpenModals { get; init; } = [];
    public bool IsOpen(ModalType modal) => OpenModals.Contains(item: modal);
}
