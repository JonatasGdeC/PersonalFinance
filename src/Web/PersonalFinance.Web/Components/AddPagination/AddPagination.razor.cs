using Microsoft.AspNetCore.Components;

namespace PersonalFinance.Web.Components.AddPagination;

public partial class AddPagination
{
    [Parameter] public int TotalPages { get; set; } = 1;
    [Parameter] public int CurrentPage { get; set; } = 1;
    [Parameter] public EventCallback<int> OnPageChanged { get; set; }

    private async Task GoToPage(int page)
    {
        if (page < 1 || page > TotalPages || page == CurrentPage)
        {
            return;
        }

        if (OnPageChanged.HasDelegate)
        {
            await OnPageChanged.InvokeAsync(arg: page);
        }
    }
}
