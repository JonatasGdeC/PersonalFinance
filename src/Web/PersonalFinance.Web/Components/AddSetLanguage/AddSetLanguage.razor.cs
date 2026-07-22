using Microsoft.JSInterop;
using PersonalFinance.Web.Components.AddInput;

namespace PersonalFinance.Web.Components.AddSetLanguage;

public partial class AddSetLanguage
{
    private readonly List<AddInputOption> _languages =
    [
        new() { Value = "en-US", Label = "English" },
        new() { Value = "pt-BR", Label = "Português" },
    ];

    private string _currentCulture = "en-US";

    protected override async Task OnInitializedAsync()
    {
        string? stored = await JsRuntime.InvokeAsync<string?>(identifier: "localStorage.getItem", args: ["culture"]);

        _currentCulture = stored switch
        {
            "en" => "en-US",
            "pt" => "pt-BR",
            null => System.Globalization.CultureInfo.CurrentUICulture.Name,
            _ => stored
        };
    }

    private async Task NavigateToCulture()
    {
        await JsRuntime.InvokeVoidAsync(identifier: "localStorage.setItem", args: ["culture", _currentCulture]);
        NavigationManager.NavigateTo(uri: NavigationManager.Uri, forceLoad: true);
    }
}
