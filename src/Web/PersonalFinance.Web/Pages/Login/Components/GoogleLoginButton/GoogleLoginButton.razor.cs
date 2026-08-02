using System.Globalization;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using PersonalFinance.Adapter.Exceptions;
using PersonalFinance.Communication.Requests.User;
using PersonalFinance.Communication.Responses.User;
using PersonalFinance.Web.Resources.Login;
using PersonalFinance.Web.Services.SnackbarService;

namespace PersonalFinance.Web.Pages.Login.Components.GoogleLoginButton;

public partial class GoogleLoginButton : ComponentBase, IDisposable
{
    private const string CONTAINER_ID = "google-login-button-slot";

    [Inject] private IConfiguration Configuration { get; set; } = default!;

    private DotNetObjectReference<GoogleLoginButton>? _reference;
    private List<string> _errorMessage = [];

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
        {
            return;
        }

        string? clientId = Configuration[key: "GoogleClientId"];

        if (string.IsNullOrWhiteSpace(value: clientId))
        {
            return;
        }

        _reference = DotNetObjectReference.Create(value: this);

        await JsRuntime.InvokeVoidAsync(
            identifier: "googleAuth.renderButton",
            clientId,
            CONTAINER_ID,
            _reference,
            CultureInfo.CurrentUICulture.Name);
    }

    [JSInvokable]
    public async Task OnGoogleCredential(string credential)
    {
        _errorMessage = [];

        try
        {
            LoginResponse response = await PersonalFinanceApi.User.LoginGoogle(request: new LoginGoogleRequest { IdToken = credential });
            await AuthenticationStateProvider.SetTokenAsync(token: response.Token);
            NavigationManager.NavigateTo(uri: "/");
            SnackbarService.Show(message: LoginResources.WelcomeBack, severity: SnackbarSeverity.Success);
        }
        catch (ApiException exception) when (exception.ErrorMessages.Count > 0)
        {
            _errorMessage = exception.ErrorMessages.ToList();
        }
        catch
        {
            SnackbarService.Show(message: LoginResources.UnknownError, severity: SnackbarSeverity.Error);
        }

        StateHasChanged();
    }

    public void Dispose() => _reference?.Dispose();
}
