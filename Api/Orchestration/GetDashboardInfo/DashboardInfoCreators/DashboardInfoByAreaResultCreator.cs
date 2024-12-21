namespace Api.Orchestration.GetDashboardInfo.DashboardInfoCreators;

public class DashboardInfoByAreaResultCreator : DashboardInfoCreatorBase
{
    public DashboardInfoByAreaResultCreator(Location location) : base(location) { }

    public override DashboardInfoResponseDto GetResult(List<Location> locations)
    {
        var locationsByState = locations.Where(oo => oo.State == _location.State).ToList();
        var locationsByArea = locations.Where(oo => oo.Area == _location.Area).ToList();
        var stateNavigationItem = new NavigationItem(_location.State, locationsByState.Count, isOpen: true);
        return GetBreadCrumb(locations, stateNavigationItem);
    }

    protected override NavigationItem GetAreaNavItem(IGrouping<string, Location> location, List<NavigationItem> cities)
    {
        var isSelected = location.Key == _location.Area;
        return GetNavItem(location, isSelected, isSelected, cities);
    }

    protected override NavigationItem GetCityNavItem(IGrouping<string, Location> location)
    {
        return GetNavItem(location, false, false);
    }

    protected override (string name, string nameUrl, string description) GetUrlAndDescriptionProperties()
    {
        var name = _location.Area;
        var nameUrl = _location.AreaUrlFriendly;
        var description = _location.AreaDescription;
        return (name, nameUrl, description);
    }
}
