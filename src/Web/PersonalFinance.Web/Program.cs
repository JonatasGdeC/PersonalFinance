using System.Globalization;
using ApexCharts;
using Fluxor;
using Fluxor.Blazor.Web.ReduxDevTools;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using PersonalFinance.Adapter;
using PersonalFinance.Web;

WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args: args);
builder.RootComponents.Add<App>(selector: "#app");
builder.RootComponents.Add<HeadOutlet>(selector: "head::after");

builder.Services.AddScoped(implementationFactory: sp => new HttpClient { BaseAddress = new Uri(uriString: builder.HostEnvironment.BaseAddress) });
builder.Services.AddAdapter(builder: builder);
builder.Services.AddApexCharts();

builder.Services.AddFluxor(configure: options =>
{
    options.ScanAssemblies(assemblyToScan: typeof(Program).Assembly);
    options.UseReduxDevTools();
});

WebAssemblyHost host = builder.Build();

string? storedCulture = await host.Services.GetRequiredService<IJSRuntime>().InvokeAsync<string?>(identifier: "localStorage.getItem", args: ["culture"]);

string cultureName = storedCulture switch
{
    "en" => "en-US",
    "pt" => "pt-BR",
    null => CultureInfo.CurrentUICulture.Name,
    _ => storedCulture
};

CultureInfo culture = new(name: cultureName);
CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

await host.RunAsync();