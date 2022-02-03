using System;
using System.Collections.Generic;
using System.Linq;

namespace Core;

public static class LocationHelper
{
	static LocationHelper()
	{
		_locations = _stateAreaCities.Select(MapToLocation).ToList();
	}

	private static Location MapToLocation((string state, string area, string city) locationTuple)
	{
		return new Location(locationTuple.state, locationTuple.area, locationTuple.city);
	}

	private static readonly List<Location> _locations;

	/// <summary>
	/// Calls First() under the hood.
	/// </summary>
	/// <param name="filterPredicate"></param>
	/// <returns></returns>
	public static Location Find(Func<Location, bool> filterPredicate) => _locations.First(filterPredicate);

	public static Location GetRandomLocation() => _locations.GetRandomValue();

	public static List<Location> GetAllLocations() => _locations;

	private static readonly (string state, string area, string city)[] _stateAreaCities = new[]
	{
		("Alabama", "Greater Montgomery Area", "Montgomery"),
		("Alabama", "Greater Montgomery Area", "Montgomery Suburbs"),
		("Alaska", "Greater Juno Area", "Juno"),
		("Arizona", "Greater Phoenix Area", "Phoenix"),
		("Arizona", "Greater Phoenix Area", "Tempe"),
		("Arizona", "Greater Phoenix Area", "Scottsdale"),
		("Arkansas", "Greater Little Rock Area", "Little Rock"),
		("California", "Greater San Diego Area", "San Diego"),
		("California", "Greater San Diego Area", "La Jolla"),
		("California", "Greater San Diego Area", "Oceanside"),
		("California", "Greater San Diego Area", "Chula Vista"),
		("Colorado", "Greater Denver Area", "Denver"),
		("Colorado", "Greater Denver Area", "Boulder"),
		("Colorado", "Greater Denver Area", "Broomfield"),
		("Colorado", "Greater Denver Area", "Westminster"),
		("Colorado", "Greater Denver Area", "Morrison"),
		("Colorado", "Greater Colorado Springs Area", "Colorado Springs"),
		("Connecticut", "Greater Hartford Area", "Hartford"),
		("Delaware", "Greater Dover Area", "Dover"),
		("Florida", "Greater Miami Area", "Miami"),
		("Florida", "Greater Miami Area", "Ft. Lauderdale"),
		("Florida", "Florida North", "Destin"),
		("Florida", "Florida North", "Jacksonville"),
		("Florida", "Florida Central", "Orlando"),
		("Florida", "Florida Central", "Tampa Bay"),
		("Georgia", "Greater Atlanta Area", "Atlanta"),
		("Georgia", "Greater Atlanta Area", "Alpharetta"),
		("Georgia", "Greater Atlanta Area", "Marietta"),
		("Georgia", "Georgia South", "Valdosta"),
		("Georgia", "Georgia South", "Macon"),
		("Georgia", "Georgia West", "Athens"),
		("Hawaii", "Greater Honolulu", "Honolulu"),
		("Idaho", "Greater Boise Area", "Boise"),
		("Illinois", "Greater Chicago Area", "Chicago"),
		("Illinois", "Greater Chicago Area", "Naperville"),
		("Illinois", "Greater Chicago Area", "Western Suburbs"),
	};
}
