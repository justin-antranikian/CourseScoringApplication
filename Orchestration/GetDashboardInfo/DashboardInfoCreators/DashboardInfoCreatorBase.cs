namespace Orchestration.GetDashboardInfo;

public abstract class DashboardInfoCreatorBase
{
	protected readonly Location _location;

	protected DashboardInfoCreatorBase(Location location)
	{
		_location = location;
	}

	public abstract DashboardInfoResponseDto GetResult(List<Location> locations);

	protected abstract NavigationItem GetCityNavItem(IGrouping<string, Location> location);

	protected abstract NavigationItem GetAreaNavItem(IGrouping<string, Location> location, List<NavigationItem> cities);

	protected abstract (string name, string nameUrl, string description) GetUrlAndDescriptionProperties();

	protected DashboardInfoResponseDto GetBreadCrumb(List<Location> locations, NavigationItem stateNavItem)
	{
		var locationsForState = locations.Where(oo => oo.State == _location.State).ToList();
		var states = GetStates(locations.GroupBy(oo => oo.State));
		var areas = GetAreas(locationsForState.GroupBy(oo => oo.Area));
		var locationInfoWithUrl = new LocationInfoWithUrl(_location.State, _location.Area, _location.City);
		var (name, nameUrl, description) = GetUrlAndDescriptionProperties();
		return new DashboardInfoResponseDto(states, areas, stateNavItem, name, nameUrl, description);
	}

	protected static NavigationItem GetNavItem(IGrouping<string, Location> location, bool isOpen = false, bool isHighlighted = false, List<NavigationItem>? items = null)
	{
		return new NavigationItem(location.Key, location.Count(), isOpen, isHighlighted, items);
	}

	private static List<NavigationItem> GetStates(IEnumerable<IGrouping<string, Location>> byStateGrouping)
	{
		return DashboardInfoHelper.GetStates(byStateGrouping);
	}

	private List<NavigationItem> GetAreas(IEnumerable<IGrouping<string, Location>> byAreaGrouping)
	{
		var areas = byAreaGrouping.Select(oo =>
		{
			var cities = GetCities(oo.GroupBy(oo => oo.City));
			return GetAreaNavItem(oo, cities);
		});

		return areas.OrderBy(oo => oo.DisplayName).ToList();
	}

	private List<NavigationItem> GetCities(IEnumerable<IGrouping<string, Location>> byCityGrouping)
	{
		var navItems = byCityGrouping.Select(oo => GetCityNavItem(oo));
		return navItems.OrderBy(oo => oo.DisplayName).ToList();
	}
}
