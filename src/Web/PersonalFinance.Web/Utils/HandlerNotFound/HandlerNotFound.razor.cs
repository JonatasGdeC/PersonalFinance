using Microsoft.AspNetCore.Components;

namespace PersonalFinance.Web.Utils.HandlerNotFound;

public partial class HandlerNotFound : ComponentBase
{
    [Parameter] public required string Text { get; init; } = "Not found.";
    [Parameter] public int Height { get; init; } = 400;
}