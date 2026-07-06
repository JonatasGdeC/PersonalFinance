using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PersonalFinance.Web;

WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args: args);
builder.RootComponents.Add<App>(selector: "#app");
builder.RootComponents.Add<HeadOutlet>(selector: "head::after");

builder.Services.AddScoped(implementationFactory: sp => new HttpClient { BaseAddress = new Uri(uriString: builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();