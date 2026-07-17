using System.Globalization;
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

WebAssemblyHost host = builder.Build();

string? storedCulture = await host.Services.GetRequiredService<IJSRuntime>()
    .InvokeAsync<string?>(identifier: "localStorage.getItem", args: ["culture"]);

CultureInfo culture = new(name: storedCulture ?? CultureInfo.CurrentUICulture.Name);
CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

await host.RunAsync();