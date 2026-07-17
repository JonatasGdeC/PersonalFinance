using Microsoft.AspNetCore.Components;

namespace PersonalFinance.Web.Components.AddCard;

public partial class AddCard : ComponentBase
{
    [Parameter] public required RenderFragment Body { get; set; }
    [Parameter] public string Width { get; set; } = "auto";
    [Parameter] public string Height { get; set; } = "auto";
    [Parameter] public string BorderRadius { get; set; } = "12px";
    [Parameter] public string BackgroundColor { get; set; } = "var(--white)";
    [Parameter] public string Padding { get; set; } = "32px";
    [Parameter] public bool IsLoading { get; set; }

    private string HandleStyle()
    {
        string width = $"width: {Width};";
        string height = $"height: {Height};";
        string borderRadius = $"border-radius: {BorderRadius};";
        string backgroundColor = $"background-color: {BackgroundColor};";
        string padding = $"padding: {Padding};";

        return $"{width} {height} {borderRadius} {backgroundColor} {padding}";
    }
}