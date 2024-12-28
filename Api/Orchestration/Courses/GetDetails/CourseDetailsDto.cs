namespace Api.Orchestration.Courses.GetDetails;

public record CourseDetailsDto
{
    public required int CourseId { get; init; }
    public required string CourseName { get; init; }
    public required int RaceId { get; init; }
    public required string RaceName { get; init; }
    public required string CourseType { get; init; }
    public required double Distance { get; init; }
    public required string Name { get; init; }
    public required string PaceType { get; init; }
    public required string PreferedMetric { get; init; }
    public required int SortOrder { get; init; }
    public required LocationInfoWithRank LocationInfoWithRank { get; init; }
}