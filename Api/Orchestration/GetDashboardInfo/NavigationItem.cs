using Core;

namespace Api.Orchestration.GetDashboardInfo;

public class NavigationItem(string displayName, int count, bool isOpen = false, bool isHighlighted = false, List<NavigationItem>? items = null)
{
    public string DisplayName { get; } = displayName;

    public string DisplayNameUrl { get; } = displayName.ToUrlFriendlyText();

    public int Count { get; } = count;

    public bool IsOpen { get; } = isOpen;

    public bool IsHighlighted { get; } = isHighlighted;

    public List<NavigationItem>? Items { get; } = items;
}
