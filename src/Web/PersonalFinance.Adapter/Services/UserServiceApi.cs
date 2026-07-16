using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Requests.User;
using PersonalFinance.Communication.Responses.User;

namespace PersonalFinance.Adapter.Services;

public class UserServiceApi(HttpClient httpClient) : ApiServiceBase(httpClient: httpClient)
{
    private const string BaseUri = "User";

    public async Task<RegisterUserResponse> Register(RegisterUserRequest request) =>
        await PostAsync<RegisterUserRequest, RegisterUserResponse>(uri: BaseUri, request: request);

    public async Task<LoginResponse> Login(LoginRequest request) =>
        await PostAsync<LoginRequest, LoginResponse>(uri: $"{BaseUri}/login", request: request);

    public async Task<UserDto?> Get() => 
        await GetAsync<UserDto>(uri: BaseUri);

    public async Task Update(UpdateUserRequest request) => 
        await PutAsync(uri: BaseUri, request: request);
    
    public async Task UpdatePassword(UpdatePasswordRequest request) => 
        await PutAsync(uri: $"{BaseUri}/password", request: request);

    public async Task ForgotPassword(ForgotPasswordRequest request) => 
        await PostAsync<ForgotPasswordRequest, object>(uri: $"{BaseUri}/forgot-password", request: request);

    public async Task<ValidateResetCodeResponse> ValidateResetCode(ValidateResetCodeRequest request) =>
        await PostAsync<ValidateResetCodeRequest, ValidateResetCodeResponse>(uri: $"{BaseUri}/validate-reset-code", request: request);

    public async Task ResetPassword(ResetPasswordRequest request) => 
        await PutAsync(uri: $"{BaseUri}/reset-password", request: request);

    public async Task Delete() => 
        await DeleteAsync(uri: BaseUri);
}