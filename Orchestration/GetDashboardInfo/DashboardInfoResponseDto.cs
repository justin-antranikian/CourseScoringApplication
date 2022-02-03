namespace Orchestration.GetDashboardInfo;

public class DashboardInfoResponseDto
{
	public List<NavigationItem> States { get; }

	public List<NavigationItem>? Areas { get; }

	public NavigationItem? StateNavigationItem { get; }

	public string? Title { get; }

	public string? TitleUrl { get; }

	public string? Description { get; }

	public DashboardInfoResponseDto(List<NavigationItem> states)
	{
		States = states;
	}

	public DashboardInfoResponseDto(List<NavigationItem> states, List<NavigationItem> areas, NavigationItem stateNavigationItem, string title, string titleUrl, string description) : this(states)
	{
		Areas = areas;
		StateNavigationItem = stateNavigationItem;
		Title = title;
		TitleUrl = titleUrl;
		Description = description;
	}
}
