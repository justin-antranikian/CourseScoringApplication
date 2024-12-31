namespace Api.Orchestration.Courses.GetDetails;

public record CourseDetailsDto
{
    public required int Id { get; init; }

    public required string CourseDate { get; init; }
    public required string CourseTime { get; init; }
    public required double Distance { get; init; }
    public required LocationInfoWithRank LocationInfoWithRank { get; init; }
    public required string Name { get; init; }
    public required int RaceId { get; init; }
    public required string RaceName { get; init; }
    public required string RaceSeriesType { get; init; }
}
