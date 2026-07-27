using PersonalFinance.Communication.Dtos;

namespace PersonalFinance.Web.UseState.User;

public abstract class UserActions
{
    public record GetCurrentUserAction;
    public record GetCurrentUserSuccessAction(UserDto User);
    public record UpdateCurrentUserSuccessAction(UserDto User);
}
