using Api.DataModels.Enums;

namespace Api.Orchestration.GetArp;

public record AthleteWellnessEntryDto
{
    public required AthleteWellnessType AthleteWellnessType { get; set; }
    public required string Description { get; set; }
}
