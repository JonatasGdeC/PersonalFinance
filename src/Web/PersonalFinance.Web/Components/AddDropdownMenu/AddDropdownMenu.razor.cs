using Microsoft.AspNetCore.Components;

namespace PersonalFinance.Web.Components.AddDropdownMenu;

public partial class AddDropdownMenu : ComponentBase
{
    [Parameter] public bool IsOpen { get; set; }
    [Parameter] public EventCallback OnClose { get; set; }
    [Parameter] public string TextOption1 { get; set; } = string.Empty;
    [Parameter] public string TextOption2 { get; set; } = string.Empty;
    [Parameter] public EventCallback OnClick1 { get; set; }
    [Parameter] public EventCallback OnClick2 { get; set; }
    [Parameter] public bool Option2IsDestructive { get; set; }

    private async Task HandleClose()
    {
        if (OnClose.HasDelegate)
        {
            await OnClose.InvokeAsync();
        }
    }
    
    private async Task HandleClick1() => await OnClick1.InvokeAsync();
    private async Task HandleClick2() => await OnClick2.InvokeAsync();
}
