using Microsoft.AspNetCore.Components;
using PersonalFinance.Web.Resources.Common;

namespace PersonalFinance.Web.Utils.HandlerNotFound;

public partial class HandlerNotFound : ComponentBase
{
    [Parameter] public required string Text { get; init; } = CommonResources.NotFoundDefaultText;
    [Parameter] public int Height { get; init; } = 400;
}