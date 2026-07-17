using Microsoft.AspNetCore.Components;
using PersonalFinance.Web.Extensions;

namespace PersonalFinance.Web.Components.AddText;

public partial class AddText
{
    [Parameter] public required string Text { get; set; }
    [Parameter] public TextPreset Preset { get; set; } = TextPreset.Preset1;
    [Parameter] public TagText TagHtml { get; set; } = TagText.P;
    [Parameter] public ThemeColor Color { get; set; } = ThemeColor.Grey900;
    
    private string GetPresetCssClass()
    {
        return Preset switch
        {
            TextPreset.Preset1     => "add-text--text-preset-1",
            TextPreset.Preset2     => "add-text--text-preset-2",
            TextPreset.Preset3     => "add-text--text-preset-3",
            TextPreset.Preset4     => "add-text--text-preset-4",
            TextPreset.Preset4Bold => "add-text--text-preset-4-bold",
            TextPreset.Preset5     => "add-text--text-preset-5",
            TextPreset.Preset5Bold => "add-text--text-preset-5-bold",
            _                      => throw new ArgumentOutOfRangeException(paramName: nameof(Preset), actualValue: Preset, message: null)
        };
    }

    private string HandleColor => $"color: {Color.ToCssVar()}";
}

public enum TextPreset
{
    Preset1,
    Preset2,
    Preset3,
    Preset4,
    Preset4Bold,
    Preset5,
    Preset5Bold
}

public enum TagText
{
    P,
    H1,
    H2,
    H3,
    H4,
    H5,
    Label,
}