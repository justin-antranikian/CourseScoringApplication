namespace Orchestration.GetDashboardInfo;

public class NavigationItem
{
    public string DisplayName { get; }

    public string DisplayNameUrl { get; }

    public int Count { get; }

    public bool IsOpen { get; }

    public bool IsHighlighted { get; }

    public List<NavigationItem>? Items { get; }

    public NavigationItem(string displayName, int count, bool isOpen = false, bool isHighlighted = false, List<NavigationItem>? items = null)
    {
        DisplayName = displayName;
        DisplayNameUrl = displayName.ToUrlFriendlyText();
        Count = count;
        IsOpen = isOpen;
        IsHighlighted = isHighlighted;
        Items = items;
    }
}
