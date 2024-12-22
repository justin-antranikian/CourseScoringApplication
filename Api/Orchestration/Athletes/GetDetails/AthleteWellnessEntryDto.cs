using Api.DataModels;

namespace Api.Orchestration.Athletes.GetDetails;

public record AthleteWellnessEntryDto
{
    public required AthleteWellnessType AthleteWellnessType { get; set; }
    public required string Description { get; set; }
}
