using Microsoft.AspNetCore.Components;

namespace PersonalFinance.Web.Components.AddButton;

public partial class AddButton
{
    [Parameter] public required string Text { get; set; }
    [Parameter] public ButtonVariant Variant { get; set; } = ButtonVariant.Primary;
    [Parameter] public bool Disabled { get; set; }
    [Parameter] public bool FullWidth { get; set; }
    [Parameter] public string Type { get; set; } = "button";
    [Parameter] public EventCallback OnClick { get; set; }

    private string GetVariantCssClass()
    {
        return Variant switch
        {
            ButtonVariant.Primary     => "add-button--primary",
            ButtonVariant.Secondary   => "add-button--secondary",
            ButtonVariant.Tertiary    => "add-button--tertiary",
            ButtonVariant.Destructive => "add-button--destructive",
            _                         => throw new ArgumentOutOfRangeException(paramName: nameof(Variant), actualValue: Variant, message: null)
        };
    }

    private string GetCssClass()
    {
        string cssClass = $"add-button {GetVariantCssClass()}";
        return FullWidth ? $"{cssClass} add-button--full-width" : cssClass;
    }

    private async Task HandleClick()
    {
        if (Disabled || !OnClick.HasDelegate)
        {
            return;
        }

        await OnClick.InvokeAsync();
    }
}

public enum ButtonVariant
{
    Primary,
    Secondary,
    Tertiary,
    Destructive
}
