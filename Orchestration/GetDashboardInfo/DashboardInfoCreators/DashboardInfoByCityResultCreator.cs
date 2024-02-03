namespace Orchestration.GetDashboardInfo;

public class DashboardInfoByCityResultCreator : DashboardInfoCreatorBase
{
    public DashboardInfoByCityResultCreator(Location location) : base(location) { }

    public override DashboardInfoResponseDto GetResult(List<Location> locations)
    {
        var locationsByState = locations.Where(oo => oo.State == _location.State).ToList();
        var locationsByCity = locations.Where(oo => oo.City == _location.City).ToList();
        var stateNavigationItem = new NavigationItem(_location.State, locationsByState.Count, isOpen: true);
        return GetBreadCrumb(locations, stateNavigationItem);
    }

    protected override NavigationItem GetCityNavItem(IGrouping<string, Location> location)
    {
        var isSelected = location.Key == _location.City;
        return GetNavItem(location, false, isSelected);
    }

    protected override NavigationItem GetAreaNavItem(IGrouping<string, Location> location, List<NavigationItem> cities)
    {
        var isSelected = location.Key == _location.Area;
        return GetNavItem(location, isSelected, false, cities);
    }

    protected override (string name, string nameUrl, string description) GetUrlAndDescriptionProperties()
    {
        var name = _location.City;
        var nameUrl = _location.CityUrlFriendly;
        var description = _location.CityDescription;
        return (name, nameUrl, description);
    }
}
