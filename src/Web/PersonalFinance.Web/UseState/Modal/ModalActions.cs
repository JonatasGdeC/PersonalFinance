namespace PersonalFinance.Web.UseState.Modal;

public abstract class ModalActions
{
    public record OpenModalAction(ModalType Modal);
    public record CloseModalAction(ModalType Modal);
}
