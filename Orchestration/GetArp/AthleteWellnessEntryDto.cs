using Core.Enums;

namespace Orchestration.GetArp;

public record AthleteWellnessEntryDto
{
    public required AthleteWellnessType AthleteWellnessType { get; set; }
    public required string Description { get; set; }
}
