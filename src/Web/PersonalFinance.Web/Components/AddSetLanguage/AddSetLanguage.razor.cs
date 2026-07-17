using Microsoft.JSInterop;
using PersonalFinance.Web.Components.AddInput;

namespace PersonalFinance.Web.Components.AddSetLanguage;

public partial class AddSetLanguage
{
    private readonly List<AddInputOption> _languages =
    [
        new() { Value = "en", Label = "English" },
        new() { Value = "pt", Label = "Português" },
    ];

    private string _currentCulture = "en";

    protected override async Task OnInitializedAsync()
    {
        string? stored = await JsRuntime.InvokeAsync<string?>(identifier: "localStorage.getItem", args: ["culture"]);
        _currentCulture = stored ?? System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
    }

    private async Task NavigateToCulture()
    {
        await JsRuntime.InvokeVoidAsync(identifier: "localStorage.setItem", args: ["culture", _currentCulture]);
        NavigationManager.NavigateTo(uri: NavigationManager.Uri, forceLoad: true);
    }
}
