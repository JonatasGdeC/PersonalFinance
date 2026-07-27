using Microsoft.AspNetCore.Components;

namespace PersonalFinance.Web.Components.AddDropdownMenu;

public partial class AddDropdownMenu : ComponentBase
{
    [Parameter] public bool IsOpen { get; set; }
    [Parameter] public EventCallback OnClose { get; set; }
    [Parameter] public required RenderFragment ChildContent { get; set; }

    private async Task HandleClose()
    {
        if (OnClose.HasDelegate)
        {
            await OnClose.InvokeAsync();
        }
    }
}
