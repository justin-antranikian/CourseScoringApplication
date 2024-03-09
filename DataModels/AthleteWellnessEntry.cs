using Core.Enums;

namespace DataModels;

public class AthleteWellnessEntry
{
    public int Id { get; set; }
    public int AthleteId { get; set; }

    public required AthleteWellnessType AthleteWellnessType { get; set; }
    public required string Description { get; set; }

    public Athlete Athlete { get; set; }
}
