using Core;
using System.Collections.Generic;
using System.Linq;

namespace Orchestration.GetDashboardInfo
{
	public static class DashboardInfoHelper
	{
		public static List<NavigationItem> GetStates(IEnumerable<IGrouping<string, Location>> byStateGrouping)
		{
			return byStateGrouping
					.Select(oo => new NavigationItem(oo.Key, oo.Count()))
					.OrderBy(oo => oo.DisplayName)
					.ToList();
		}
	}
}
