namespace Api.DataModels;

public class AthleteRaceSeriesGoal
{
    public int Id { get; set; }
    public int AthleteId { get; set; }

    public required RaceSeriesType RaceSeriesType { get; set; }
    public required int TotalEvents { get; set; }

    public Athlete? Athlete { get; set; }

    public static AthleteRaceSeriesGoal Create(RaceSeriesType raceSeriesType, int totalEvents)
    {
        return new AthleteRaceSeriesGoal
        {
            RaceSeriesType = raceSeriesType,
            TotalEvents = totalEvents
        };
    }
}
