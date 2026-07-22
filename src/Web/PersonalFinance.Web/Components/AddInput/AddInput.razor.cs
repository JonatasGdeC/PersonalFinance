using Microsoft.AspNetCore.Components;
using PersonalFinance.Web.Components.AddIcon;

namespace PersonalFinance.Web.Components.AddInput;

public partial class AddInput
{
    [Parameter] public string? Label { get; set; }
    [Parameter] public string? Placeholder { get; set; }
    [Parameter] public string? HelperText { get; set; }
    [Parameter] public string? Prefix { get; set; }
    [Parameter] public bool ShowSearchIcon { get; set; }
    [Parameter] public bool IsPassword { get; set; }
    [Parameter] public bool IsEmail { get; set; }
    [Parameter] public bool IsDate { get; set; }
    [Parameter] public Icon? CompactIcon { get; set; }
    [Parameter] public List<AddInputOption>? Options { get; set; }
    [Parameter] public string? Value { get; set; }
    [Parameter] public EventCallback<string?> ValueChanged { get; set; }
    [Parameter] public bool Disabled { get; set; }
    [Parameter] public bool Required { get; set; }
    [Parameter] public bool IsLoading { get; set; }

    private bool _isOpen;
    private bool _isPasswordVisible;
    
    private readonly string _addInputId = Guid.NewGuid().ToString();
    
    private bool IsDropdown => Options is { Count: > 0 };

    private AddInputOption? SelectedOption => Options?.FirstOrDefault(predicate: option => option.Value == Value);

    private string GetInputType() => IsPassword && !_isPasswordVisible ? "password" : IsEmail ? "email" : IsDate ? "date" : "text";

    private void TogglePasswordVisibility() => _isPasswordVisible = !_isPasswordVisible;

    private void ToggleDropdown()
    {
        if (Disabled)
        {
            return;
        }

        _isOpen = !_isOpen;
    }

    private void CloseDropdown() => _isOpen = false;

    private async Task SelectOption(AddInputOption option)
    {
        Value = option.Value;
        _isOpen = false;

        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(arg: Value);
        }
    }

    private async Task HandleValueChanged(ChangeEventArgs e)
    {
        Value = e.Value?.ToString();

        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(arg: Value);
        }
    }
}
