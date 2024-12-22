namespace Api.DataModels;

public class AthleteRaceSeriesGoal(RaceSeriesType raceSeriesType, int totalEvents)
{
    public int Id { get; set; }
    public int AthleteId { get; set; }

    public RaceSeriesType RaceSeriesType { get; set; } = raceSeriesType;
    public int TotalEvents { get; set; } = totalEvents;

    public Athlete Athlete { get; set; }
}
