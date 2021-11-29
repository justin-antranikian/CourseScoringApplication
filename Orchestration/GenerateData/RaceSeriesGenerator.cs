using Bogus;
using Core;
using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Orchestration.GenerateData
{
	internal class RaceSeriesFaker
	{
		public Guid Identifier;
		public string Name;
		public RaceSeriesType RaceSeriesType;
	}

	internal class RaceSeriesBasic : RaceSeriesFaker
	{
		public readonly int Rank;
		public readonly string State;
		public readonly string Area;
		public readonly string City;

		public RaceSeriesBasic(RaceSeriesFaker raceSeriesBasic, Location location, int rank)
		{
			Identifier = raceSeriesBasic.Identifier;
			Name = raceSeriesBasic.Name;
			RaceSeriesType = raceSeriesBasic.RaceSeriesType;
			Rank = rank;
			State = location.State;
			Area = location.Area;
			City = location.City;
		}
	}

	public static class RaceSeriesGenerator
	{
		public static List<RaceSeries> GetRaceSeries()
		{
			var raceSeriesBasicEntries = GetRaceSeriesBasicEntries();

			var stateDictionary = GetRankingDictionary(raceSeriesBasicEntries, oo => oo.State);
			var areaDictionary = GetRankingDictionary(raceSeriesBasicEntries, oo => oo.Area);
			var cityDictionary = GetRankingDictionary(raceSeriesBasicEntries, oo => oo.City);

			var possibleDescriptions = new[]
			{
				"The intense ride through the desert. Sponsered by Black Diamond and REI",
				"The best event that money can buy. Sponsered by Red Bull.",
				"A great time through a wonderful location. Sponsered by REI.",
				"Help save a life by raising money for sick people. Sponsered Spectrum Health.",
				"Get your year started off right with this event. Sponsered Lifetime Fitness.",
				"A great event through the city. Brought to you by our sponsers.",
			};

			var possibleUpcomingValues = new[] { true, false };
			var possibleRatings = Enumerable.Range(1, 10);

			RaceSeries MapToRaceSeries(RaceSeriesBasic raceSeriesBasic)
			{
				var id = raceSeriesBasic.Identifier;

				return new RaceSeries
				{
					Name = raceSeriesBasic.Name,
					Description = possibleDescriptions.GetRandomValue(),
					RaceSeriesType = raceSeriesBasic.RaceSeriesType,
					State = raceSeriesBasic.State,
					Area = raceSeriesBasic.Area,
					City = raceSeriesBasic.City,
					OverallRank = raceSeriesBasic.Rank,
					StateRank = stateDictionary[id],
					AreaRank = areaDictionary[id],
					CityRank = cityDictionary[id],
					IsUpcoming = possibleUpcomingValues.GetRandomValue(),
					Rating = possibleRatings.GetRandomValue()
				};
			}

			var raceSeriesEntries = raceSeriesBasicEntries.Select(MapToRaceSeries).ToList();
			return raceSeriesEntries;
		}

		private static List<RaceSeriesBasic> GetRaceSeriesBasicEntries()
		{
			var raceSeriesFaker = new Faker<RaceSeriesFaker>()
				.RuleFor(oo => oo.Identifier, f => Guid.NewGuid())
				.RuleFor(oo => oo.Name, f => f.Address.City())
				.RuleFor(oo => oo.RaceSeriesType, f => typeof(RaceSeriesType).GetRandomEnumValue()
			);

			var raceSeriesBasicEntries = raceSeriesFaker.Generate(50).Select((oo, index) =>
			{
				var possibleLocation = LocationHelper.GetRandomLocation();
				var rank = index + 1;
				return new RaceSeriesBasic(oo, possibleLocation, rank);
			});

			return raceSeriesBasicEntries.ToList();
		}

		private static Dictionary<Guid, int> GetRankingDictionary(List<RaceSeriesBasic> raceSeriesEntries, Func<RaceSeriesBasic, string> keySelector)
		{
			var rankingDictionary = new Dictionary<Guid, int>();
			foreach (var grouping in raceSeriesEntries.GroupBy(keySelector))
			{
				var rank = 1;
				foreach (var raceSeriesBasic in grouping)
				{
					rankingDictionary.Add(raceSeriesBasic.Identifier, rank);
					rank++;
				}
			}

			return rankingDictionary;
		}
	}
}
