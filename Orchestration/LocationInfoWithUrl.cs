using Core;
using DataModels;

namespace Orchestration
{
	public class LocationInfoWithUrl
	{
		public string State { get; }

		public string StateUrl { get => State.ToUrlFriendlyText(); }

		public string Area { get; }

		public string AreaUrl { get => Area.ToUrlFriendlyText(); }

		public string City { get; }

		public string CityUrl { get => City.ToUrlFriendlyText(); }

		public LocationInfoWithUrl(string state, string area, string city)
		{
			State = state;
			Area = area;
			City = city;
		}

		public LocationInfoWithUrl(Location location) : this(location.State, location.Area, location.City) { }

		public LocationInfoWithUrl(RaceSeries raceSeries) : this(raceSeries.State, raceSeries.Area, raceSeries.City) { }

		public LocationInfoWithUrl(Athlete athlete) : this(athlete.State, athlete.Area, athlete.City) { }
	}
}
