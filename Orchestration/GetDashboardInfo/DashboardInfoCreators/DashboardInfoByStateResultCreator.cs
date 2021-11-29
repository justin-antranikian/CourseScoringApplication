using Core;
using System.Collections.Generic;
using System.Linq;

namespace Orchestration.GetDashboardInfo
{
	public class DashboardInfoByStateResultCreator : DashboardInfoCreatorBase
	{
		public DashboardInfoByStateResultCreator(Location location) : base(location) { }

		public override DashboardInfoResponseDto GetResult(List<Location> locations)
		{
			var locationsByState = locations.Where(oo => oo.State == _location.State).ToList();
			var stateNavigationItem = new NavigationItem(_location.State, locationsByState.Count, isOpen: true, isHighlighted: true);
			return GetBreadCrumb(locations, stateNavigationItem);
		}

		protected override NavigationItem GetAreaNavItem(IGrouping<string, Location> location, List<NavigationItem> cities)
		{
			return GetNavItem(location, false, false, cities);
		}

		protected override NavigationItem GetCityNavItem(IGrouping<string, Location> location)
		{
			return GetNavItem(location, false, false);
		}

		protected override (string name, string nameUrl, string description) GetUrlAndDescriptionProperties()
		{
			var name = _location.State;
			var nameUrl = _location.StateUrlFriendly;
			var description = _location.StateDescription;
			return (name, nameUrl, description);
		}
	}
}
