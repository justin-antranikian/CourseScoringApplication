using Core.Enums;

namespace Api.DataModels;

public record AthleteWellnessEntry
{
    public int Id { get; set; }
    public int AthleteId { get; set; }

    public AthleteWellnessType AthleteWellnessType { get; set; }
    public string Description { get; set; }

    public Athlete Athlete { get; set; }

    public AthleteWellnessEntry(AthleteWellnessType athleteWellnessType, string description)
    {
        AthleteWellnessType = athleteWellnessType;
        Description = description;
    }

    public AthleteWellnessEntry() { }
}
