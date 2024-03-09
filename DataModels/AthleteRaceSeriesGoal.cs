using Core;

namespace DataModels;

public class AthleteRaceSeriesGoal
{
    public AthleteRaceSeriesGoal(RaceSeriesType raceSeriesType, int totalEvents)
    {
        RaceSeriesType = raceSeriesType;
        TotalEvents = totalEvents;
    }

    public AthleteRaceSeriesGoal() {}

    public int Id { get; set; }
    public int AthleteId { get; set; }

    public RaceSeriesType RaceSeriesType { get; set; }
    public int TotalEvents { get; set; }

    public Athlete Athlete { get; set; }
}
