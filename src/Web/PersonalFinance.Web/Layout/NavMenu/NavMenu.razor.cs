using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using PersonalFinance.Web.Components.AddIcon;
using PersonalFinance.Web.Resources.Common;

namespace PersonalFinance.Web.Layout.NavMenu;

public partial class NavMenu : ComponentBase, IDisposable
{
    private static readonly NavItem[] NavItems =
    [
        new(Route: "/", Label: CommonResources.Overview, Icon: Icon.HOUSE),
        new(Route: "/transactions", Label: CommonResources.Transactions, Icon: Icon.ARROWS_DOWN_UP),
        new(Route: "/budgets", Label: CommonResources.Budgets, Icon: Icon.CHART_DONUT),
        new(Route: "/pots", Label: CommonResources.Pots, Icon: Icon.JAR_FILL),
        new(Route: "/recurring-bills", Label: CommonResources.RecurringBills, Icon: Icon.RECEIPT),
    ];

    private bool _isCollapsed;

    private record NavItem(string Route, string Label, Icon Icon);

    private bool PageActive(string route)
    {
        string url = $"/{NavigationManager.Uri.Replace(oldValue: NavigationManager.BaseUri, newValue: "")}";
        return route.Equals(value: "/") && url.Equals(value: "/") || !route.Equals(value: "/") && url.Contains(value: route);
    }

    private void ToggleCollapsed() => _isCollapsed = !_isCollapsed;

    protected override void OnInitialized() => NavigationManager.LocationChanged += StateHasChanged;

    private void StateHasChanged(object? sender, LocationChangedEventArgs e) => StateHasChanged();

    public void Dispose()
    {
        NavigationManager.LocationChanged -= StateHasChanged;
    }
}
