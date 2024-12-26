using Api.DataModels;
using Bogus;

namespace Api.Orchestration.GenerateData;

file class RaceSeriesFaker
{
    public required string Name { get; set; }
}

public static class RaceSeriesGenerator
{
    public static IEnumerable<RaceSeries> GetRaceSeries(List<Location> locations)
    {
        var raceSeriesFaker = new Faker<RaceSeriesFaker>()
            .RuleFor(oo => oo.Name, f => f.Address.City()
        );

        var possibleDescriptions = new[]
        {
            "The intense ride through the desert.",
            "The best event that money can buy.",
            "A great time through a wonderful location.",
            "Get your year started off right with this event.",
            "A great event through the city. Brought to you by our sponsers.",
        };

        //var more;

        var cityLocations = locations.SelectMany(oo => oo.ChildLocations).SelectMany(oo => oo.ChildLocations).ToList();

        var raceSeriesEntries = new List<RaceSeries>();

        var stateRanks = new Dictionary<int, int>();
        var areaRanks = new Dictionary<int, int>();
        var cityRanks = new Dictionary<int, int>();

        var overallRank = 1;
        foreach (var seriesFaker in raceSeriesFaker.Generate(50))
        {
            var cityLocation = cityLocations.GetRandomValue();
            var areaLocation = cityLocation.ParentLocation!;
            var stateLocationId = areaLocation.ParentLocationId!.Value;

            var raceSeriesType = Enum.GetValues(typeof(RaceSeriesType)).OfType<RaceSeriesType>().GetRandomValue();

            var stateRank = GetCount(stateRanks, stateLocationId);
            var areaRank = GetCount(areaRanks, areaLocation.Id);
            var cityRank = GetCount(cityRanks, cityLocation.Id);

            raceSeriesEntries.Add(new RaceSeries
            {
                AreaLocationId = areaLocation.Id,
                CityLocationId = cityLocation.Id,
                StateLocationId = stateLocationId,
                AreaRank = areaRank,
                CityRank = cityRank,
                Description = possibleDescriptions.GetRandomValue(),
                Name = seriesFaker.Name,
                OverallRank = overallRank,
                RaceSeriesType = raceSeriesType,
                StateRank = stateRank
            });

            overallRank++;
        }

        //var stateRankings = GetRankingDictionary(raceSeriesEntries, oo => oo.StateLocationId);
        //var areaRankings = GetRankingDictionary(raceSeriesEntries, oo => oo.AreaLocationId);
        //var cityRankings = GetRankingDictionary(raceSeriesEntries, oo => oo.CityLocationId);

        //foreach (var raceSeriesEntry in raceSeriesEntries)
        //{
        //    raceSeriesEntry.StateRank = stateRankings[raceSeriesEntry.Id];
        //    raceSeriesEntry.AreaRank = areaRankings[raceSeriesEntry.Id];
        //    raceSeriesEntry.CityRank = cityRankings[raceSeriesEntry.Id];
        //}

        return raceSeriesEntries;
    }

    private static int GetCount(Dictionary<int, int> counts, int locationId)
    {
        if (!counts.TryGetValue(locationId, out int currentCount))
        {
            counts[locationId] = 1;
            return 1;
        }

        counts[locationId] = ++currentCount;
        return currentCount;
    }

    //private static Dictionary<int, int> GetRankingDictionary(List<RaceSeries> raceSeriesEntries, Func<RaceSeries, int> keySelector)
    //{
    //    var rankingDictionary = new Dictionary<int, int>();
    //    foreach (var grouping in raceSeriesEntries.GroupBy(keySelector))
    //    {
    //        var rank = 1;
    //        foreach (var raceSeriesBasic in grouping)
    //        {
    //            rankingDictionary.Add(raceSeriesBasic.Id, rank);
    //            rank++;
    //        }
    //    }

    //    return rankingDictionary;
    //}
}
