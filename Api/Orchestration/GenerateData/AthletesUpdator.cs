using Api.DataModels;

namespace Api.Orchestration.GenerateData;

public static class AthletesUpdator
{
    public static void RankAthletes(List<Athlete> athletes, List<Location> locations)
    {
        var sortedAthletes = athletes.OrderByDescending(oo => oo.AthleteCourses.Count).ToList();

        var stateRanks = new Dictionary<int, int>();
        var areaRanks = new Dictionary<int, int>();
        var cityRanks = new Dictionary<int, int>();

        var overallRank = 1;
        foreach (var athlete in sortedAthletes)
        {
            var stateRank = GetCount(stateRanks, athlete.StateLocationId);
            var areaRank = GetCount(areaRanks, athlete.AreaLocationId);
            var cityRank = GetCount(cityRanks, athlete.CityLocationId);

            athlete.OverallRank = overallRank;
            athlete.StateRank = stateRank;
            athlete.AreaRank = areaRank;
            athlete.CityRank = cityRank;

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
