using Microsoft.AspNetCore.Components;

namespace PersonalFinance.Web.Components.AddModal;

public partial class AddModal : ComponentBase
{
    [Parameter] public bool IsOpen { get; set; }
    [Parameter] public required string Title { get; set; }
    [Parameter] public string? Subtitle { get; set; }
    [Parameter] public string MaxWidth { get; set; } = "400px";
    [Parameter] public EventCallback OnClose { get; set; }
    [Parameter] public required RenderFragment Body { get; set; }

    private async Task HandleClose()
    {
        if (OnClose.HasDelegate)
        {
            await OnClose.InvokeAsync();
        }
    }
}
