namespace Orchestration.GetBreadcrumb;

public enum BreadcrumbNavigationLevel
{
	All,
	State,
	Area,
	City,
	ArpOrRaceSeriesDashboard,
	RaceLeaderboard,
	CourseLeaderboard,
	Irp
}

public record BreadcrumbRequestDto
{
	public BreadcrumbNavigationLevel BreadcrumbNavigationLevel { get; set; }

	public string SearchTerm { get; set; }
}
