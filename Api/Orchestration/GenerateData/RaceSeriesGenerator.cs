using Api.DataModels;
using Bogus;
using NetTopologySuite.Geometries;
using Location = Api.DataModels.Location;

namespace Api.Orchestration.GenerateData;

file class RaceSeriesFaker
{
    public required string Name { get; init; }
    public required RaceSeriesType RaceSeriesType { get; init; }
}

public static class RaceSeriesGenerator
{
    private static readonly Dictionary<string, (double Latitude, double Longitude)> CityCoordinates = new()
    {
        // Alabama
        { "Montgomery", (32.3668052, -86.2999689) },
        { "Montgomery Suburbs", (32.3732, -86.3083) }, // Example coordinates
        // Arizona
        { "Phoenix", (33.4483771, -112.0740373) },
        { "Tempe", (33.4255104, -111.9400054) },
        { "Scottsdale", (33.4941704, -111.9260519) },
        // California
        { "San Diego", (32.715738, -117.1610838) },
        { "La Jolla", (32.8328112, -117.2712717) },
        { "Oceanside", (33.1958696, -117.3794834) },
        { "Chula Vista", (32.6400541, -117.0841955) },
        // Colorado
        { "Denver", (39.7392358, -104.990251) },
        { "Boulder", (40.015, -105.2705) },
        { "Broomfield", (39.9205411, -105.0866504) },
        { "Westminster", (39.8366528, -105.0372046) },
        { "Morrison", (39.6530454, -105.1911014) },
        { "Colorado Springs", (38.8338816, -104.8213634) },
        // Connecticut
        { "Hartford", (41.7658043, -72.6733723) },
        // Delaware
        { "Dover", (39.158168, -75.5243682) },
        // Florida
        { "Miami", (25.7616798, -80.1917902) },
        { "Ft. Lauderdale", (26.1224386, -80.1373174) },
        { "Destin", (30.3935337, -86.4957833) },
        { "Jacksonville", (30.3321838, -81.655651) },
        { "Orlando", (28.5383355, -81.3792365) },
        { "Tampa Bay", (27.950575, -82.4571776) },
        // Georgia
        { "Atlanta", (33.7489954, -84.3879824) },
        { "Alpharetta", (34.0753762, -84.2940899) },
        { "Marietta", (33.952602, -84.5499327) },
        { "Valdosta", (30.8327022, -83.2784851) },
        { "Macon", (32.8406946, -83.6324022) },
        { "Athens", (33.9519347, -83.357567) },
        // Hawaii
        { "Honolulu", (21.3069444, -157.8583333) },
        // Idaho
        { "Boise", (43.6150186, -116.2023137) },
        // Illinois
        { "Chicago", (41.8781136, -87.6297982) },
        { "Naperville", (41.7508391, -88.1535352) },
        { "Western Suburbs", (41.850, -87.883) } // Example coordinates
    };

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

            var (latitude, longitude) = CityCoordinates[cityLocation.Name];
            var point = GeometryExtensions.CreatePoint(latitude, longitude);

            yield return new RaceSeries
            {
                AreaLocationId = areaLocation.Id,
                CityLocationId = cityLocation.Id,
                StateLocationId = stateLocationId,
                AreaRank = areaRank,
                CityRank = cityRank,
                Location = point,
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
