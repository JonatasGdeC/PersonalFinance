using PersonalFinance.Communication.Dtos;

namespace PersonalFinance.Communication.Responses.User;

public record LoginResponse
{
    public required UserDto User { get; init; }
    public required string Token { get; init; }
}
