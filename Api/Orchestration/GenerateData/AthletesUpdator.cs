using Api.DataModels;

namespace Api.Orchestration.GenerateData;

public static class AthletesUpdator
{
    public static void RankAthletes(List<Athlete> athletes)
    {
        var sortedAthletes = athletes.OrderByDescending(oo => oo.AthleteCourses.Count).ToList();

        var stateRankings = GetRankingDictionary(sortedAthletes, oo => oo.State);
        var areaRankings = GetRankingDictionary(sortedAthletes, oo => oo.Area);
        var cityRankings = GetRankingDictionary(sortedAthletes, oo => oo.City);

        var overallRank = 1;
        foreach (var athlete in sortedAthletes)
        {
            var id = athlete.Id;
            athlete.OverallRank = overallRank;
            athlete.StateRank = stateRankings[id];
            athlete.AreaRank = areaRankings[id];
            athlete.CityRank = cityRankings[id];

            overallRank++;
        }
    }

    private static Dictionary<int, int> GetRankingDictionary(List<Athlete> athletes, Func<Athlete, string> keySelector)
    {
        var rankingDictionary = new Dictionary<int, int>();
        foreach (var grouping in athletes.GroupBy(keySelector))
        {
            var rank = 1;
            foreach (var athlete in grouping)
            {
                rankingDictionary.Add(athlete.Id, rank);
                rank++;
            }
        }

        return rankingDictionary;
    }
}
