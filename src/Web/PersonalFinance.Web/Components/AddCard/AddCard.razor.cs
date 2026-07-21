using Microsoft.AspNetCore.Components;
using PersonalFinance.Web.Extensions;

namespace PersonalFinance.Web.Components.AddCard;

public partial class AddCard : ComponentBase
{
    [Parameter] public required RenderFragment Body { get; set; }
    [Parameter] public string Width { get; set; } = "auto";
    [Parameter] public string Height { get; set; } = "auto";
    [Parameter] public string BorderRadius { get; set; } = "12px";
    [Parameter] public ThemeColor BackgroundColor { get; set; } = ThemeColor.White;
    [Parameter] public string Padding { get; set; } = "32px";
    [Parameter] public bool IsLoading { get; set; }

    private string HandleStyle()
    {
        string width = $"width: {Width};";
        string height = $"height: {Height};";
        string borderRadius = $"border-radius: {BorderRadius};";
        string backgroundColor = $"background-color: {BackgroundColor.ToCssVar()};";
        string padding = $"padding: {Padding};";

        return $"{width} {height} {borderRadius} {backgroundColor} {padding}";
    }
}