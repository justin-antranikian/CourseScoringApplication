using Core;
using System;

namespace Orchestration.GetBreadcrumb
{
	public abstract class GetBreadcrumbOrchestratorBase
	{
		protected LocationInfoWithUrl GetLocationInfoWithUrl(BreadcrumbRequestDto breadcrumbRequestDto)
		{
			var searchTerm = breadcrumbRequestDto.SearchTerm;
			Func<Location, bool> filterPredicate = breadcrumbRequestDto.BreadcrumbNavigationLevel switch
			{
				BreadcrumbNavigationLevel.State => (Location oo) => oo.StateUrlFriendly == searchTerm,
				BreadcrumbNavigationLevel.Area => (Location oo) => oo.AreaUrlFriendly == searchTerm,
				BreadcrumbNavigationLevel.City => (Location oo) => oo.CityUrlFriendly == searchTerm,
				_ => throw new NotImplementedException($"{nameof(breadcrumbRequestDto.BreadcrumbNavigationLevel)} must be set to State, Area, or City.")
			};

			var location = LocationHelper.Find(filterPredicate);
			return new LocationInfoWithUrl(location);
		}
	}
}
