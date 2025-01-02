using Api.DataModels;
using Bogus;

namespace Api.Orchestration.GenerateData;

file class RaceSeriesFaker
{
    public required string Name { get; init; }
    public required RaceSeriesType RaceSeriesType { get; init; }
}

public static class RaceSeriesGenerator
{
    public static IEnumerable<RaceSeries> GetRaceSeries(List<Location> locations)
    {
        var fakerRuleSet = new Faker<RaceSeriesFaker>()
            .RuleFor(oo => oo.Name, f => f.Address.City())
            .RuleFor(oo => oo.RaceSeriesType, _ => typeof(RaceSeriesType).GetRandomEnumValue()
        );

        var cityLocations = locations.Where(oo => oo.LocationType == LocationType.City).ToList();

        var stateRanks = new Dictionary<int, int>();
        var areaRanks = new Dictionary<int, int>();
        var cityRanks = new Dictionary<int, int>();

        var overallRank = 1;
        foreach (var faker in fakerRuleSet.Generate(50))
        {
            var cityLocation = cityLocations.GetRandomValue()!;
            var areaLocation = cityLocation.ParentLocation!;
            var stateLocationId = areaLocation.ParentLocationId!.Value;

            var stateRank = GetCount(stateRanks, stateLocationId);
            var areaRank = GetCount(areaRanks, areaLocation.Id);
            var cityRank = GetCount(cityRanks, cityLocation.Id);

            yield return new RaceSeries
            {
                AreaLocationId = areaLocation.Id,
                CityLocationId = cityLocation.Id,
                StateLocationId = stateLocationId,
                AreaRank = areaRank,
                CityRank = cityRank,
                Name = faker.Name,
                OverallRank = overallRank,
                RaceSeriesType = faker.RaceSeriesType,
                StateRank = stateRank
            };

            overallRank++;
        }
    }

    private static int GetCount(Dictionary<int, int> counts, int locationId)
    {
        if (!counts.TryGetValue(locationId, out var currentCount))
        {
            counts[locationId] = 1;
            return 1;
        }

        counts[locationId] = ++currentCount;
        return currentCount;
    }
}
