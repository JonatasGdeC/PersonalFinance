using PersonalFinance.Adapter.Interfaces;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Requests.User;
using PersonalFinance.Communication.Responses.User;

namespace PersonalFinance.Adapter.Services;

internal class UserServiceApi(HttpClient httpClient) : ApiServiceBase(httpClient: httpClient, baseUri: "User"), IUserServiceApi
{
    public async Task<RegisterUserResponse> Register(RegisterUserRequest request) =>
        await PostAsync<RegisterUserRequest, RegisterUserResponse>(request: request);

    public async Task<LoginResponse> Login(LoginRequest request) =>
        await PostAsync<LoginRequest, LoginResponse>(request: request, route: "/login");

    public async Task<UserDto?> Get() => 
        await GetAsync<UserDto>();

    public async Task Update(UpdateUserRequest request) => 
        await PutAsync(request: request);
    
    public async Task UpdatePassword(UpdatePasswordRequest request) => 
        await PutAsync(request: request, route: "/password");

    public async Task ForgotPassword(ForgotPasswordRequest request) => 
        await PostAsync<ForgotPasswordRequest, object>(request: request, route: "/forgot-password");

    public async Task<ValidateResetCodeResponse> ValidateResetCode(ValidateResetCodeRequest request) =>
        await PostAsync<ValidateResetCodeRequest, ValidateResetCodeResponse>(request: request, route: "/validate-reset-code");

    public async Task ResetPassword(ResetPasswordRequest request) => 
        await PutAsync(request: request, route: "/reset-password");

    public async Task Delete() => 
        await DeleteAsync();
}