using Api.DataModels;
using Core;

namespace Api.Orchestration.GenerateData;

public static class RaceGenerator
{
    public static IEnumerable<Race> GetRaces(List<RaceSeries> raceSeries)
    {
        var random = new Random();
        var possibleTimeZoneIds = new[] { "Pacific Standard Time", "Mountain Standard Time", "Central Standard Time", "Eastern Standard Time" };

        foreach (var series in raceSeries)
        {
            foreach (var _ in Enumerable.Range(0, 3))
            {
                var kickOffDate = new DateTime(2020, 5, random.Next(3, 21));
                yield return new Race
                {
                    RaceSeriesId = series.Id,
                    KickOffDate = kickOffDate,
                    Name = series.Name,
                    TimeZoneId = possibleTimeZoneIds.GetRandomValue()
                };
            }
        }
    }
}
