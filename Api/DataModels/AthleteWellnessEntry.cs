namespace Api.DataModels;

public enum AthleteWellnessType
{
    Goal,
    Training,
    Gear,
    Diet,
    Motivational
}

public record AthleteWellnessEntry
{
    public int Id { get; set; }
    public int AthleteId { get; set; }

    public required AthleteWellnessType AthleteWellnessType { get; set; }
    public required string Description { get; set; }

    public Athlete? Athlete { get; set; }

    public static AthleteWellnessEntry Create(AthleteWellnessType athleteWellnessType, string description)
    {
        return new AthleteWellnessEntry
        {
            AthleteWellnessType = athleteWellnessType,
            Description = description
        };
    }
}
