using Microsoft.AspNetCore.Components;

namespace PersonalFinance.Web.Components.AddSkeleton;

public partial class AddSkeleton
{
  [Parameter] public string Width { get; set; } = "100%";
  [Parameter] public string Height { get; set; } = "5px";
  [Parameter] public string BorderRadius { get; set; } = "5px";

  private string HandleStyle()
  {
    string width = $"width: {Width};";
    string height = $"height: {Height};";
    string radius = $"border-radius: {BorderRadius};";

    return $"{width} {height} {radius}";
  }
}
