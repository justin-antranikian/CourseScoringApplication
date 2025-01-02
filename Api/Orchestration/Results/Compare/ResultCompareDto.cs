namespace Api.Orchestration.Results.Compare;

public record ResultCompareDto
{
    public required int AthleteCourseId { get; init; }

    public required int CourseId { get; init; }
    public required string FullName { get; init; }
    public required string GenderAbbreviated { get; init; }
    public required LocationInfoWithRank LocationInfoWithRank { get; init; }
    public required int RaceAge { get; init; }

    public required List<ResultCompareIntervalDto> Intervals { get; init; }
}
