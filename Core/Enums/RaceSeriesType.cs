
using System;

namespace Core
{
	public enum RaceSeriesType
	{
		Running,
		Triathalon,
		RoadBiking,
		MountainBiking,
		CrossCountrySkiing,
		Swim
	}

	public static class RaceSeriesTypeExtensions
	{
		public static string ToFriendlyText(this RaceSeriesType raceSeriesType)
		{
			return raceSeriesType switch
			{
				RaceSeriesType.Running => "Running",
				RaceSeriesType.Triathalon => "Triathalon",
				RaceSeriesType.RoadBiking => "Road Biking",
				RaceSeriesType.MountainBiking => "Mountain Biking",
				RaceSeriesType.CrossCountrySkiing => "Cross Country Skiing",
				RaceSeriesType.Swim => "Swimming",
				_ => throw new NotImplementedException()
			};
		}

		public static string ToAthleteText(this RaceSeriesType raceSeriesType)
		{
			return raceSeriesType switch
			{
				RaceSeriesType.Running => "Runner",
				RaceSeriesType.Triathalon => "Triathlete",
				RaceSeriesType.RoadBiking => "Cyclist",
				RaceSeriesType.MountainBiking => "Mountain Biker",
				RaceSeriesType.CrossCountrySkiing => "Cross Country Skier",
				RaceSeriesType.Swim => "Swimmer",
				_ => throw new NotImplementedException()
			};
		}
	}
}
