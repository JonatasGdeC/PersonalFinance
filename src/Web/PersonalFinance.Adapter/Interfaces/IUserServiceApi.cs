using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Requests.User;
using PersonalFinance.Communication.Responses.User;

namespace PersonalFinance.Adapter.Services;

public interface IUserServiceApi
{
    Task<RegisterUserResponse> Register(RegisterUserRequest request);
    Task<LoginResponse> Login(LoginRequest request);
    Task<UserDto?> Get();
    Task Update(UpdateUserRequest request);
    Task UpdatePassword(UpdatePasswordRequest request);
    Task ForgotPassword(ForgotPasswordRequest request);
    Task<ValidateResetCodeResponse> ValidateResetCode(ValidateResetCodeRequest request);
    Task ResetPassword(ResetPasswordRequest request);
    Task Delete();
}
