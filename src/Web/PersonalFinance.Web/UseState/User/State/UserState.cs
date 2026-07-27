using Fluxor;
using PersonalFinance.Communication.Dtos;

namespace PersonalFinance.Web.UseState.User.State;

[FeatureState]
public record UserState
{
    public bool IsLoading { get; init; }
    public UserDto? User { get; init; }
}
