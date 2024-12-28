namespace Api.Orchestration.Results.Compare;

public record CompareIrpsAthleteInfoDto
{
    public required int CourseId { get; init; }
    public required int AthleteCourseId { get; init; }
    public required string City { get; init; }
    public required string FullName { get; init; }
    public required string GenderAbbreviated { get; init; }
    public required int RaceAge { get; init; }
    public required string State { get; init; }
    public required List<CompareIrpsIntervalDto> CompareIrpsIntervalDtos { get; init; }
}
