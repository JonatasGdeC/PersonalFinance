using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using PersonalFinance.Adapter.Auth;
using PersonalFinance.Adapter.Services;

namespace PersonalFinance.Adapter;

public static class DependencyInjection
{
    public static void AddAdapter(this IServiceCollection services, WebAssemblyHostBuilder builder)
    {
        string apiBaseUrl = builder.Configuration[key: "ApiBaseUrl"] ?? "http://localhost:5100";
        services.AddScoped(implementationFactory: _ => new HttpClient { BaseAddress = new Uri(uriString: apiBaseUrl) });

        services.AddScoped<PersonalFinanceApi>();

        services.AddAuthorizationCore();
        services.AddScoped<CookieAuthenticationStateProvider>();
        services.AddScoped<AuthenticationStateProvider>(implementationFactory: sp => sp.GetRequiredService<CookieAuthenticationStateProvider>());
    }
}
