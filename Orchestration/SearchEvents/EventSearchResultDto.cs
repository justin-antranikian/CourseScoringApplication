namespace Orchestration.GetRaceSeriesSearch;

public record EventSearchResultDto
{
    public required List<DisplayNameWithIdDto> Courses { get; init; }
    public required string Description { get; init; }
    public required string KickOffDate { get; init; }
    public required string KickOffTime { get; init; }
    public required int Id { get; init; }
    public required LocationInfoWithRank LocationInfoWithRank { get; init; }
    public required string Name { get; init; }
    public required RaceSeriesType RaceSeriesType { get; init; }
    public required string RaceSeriesTypeName { get; init; }
    public required int Rating { get; init; }
    public required int UpcomingRaceId { get; init; }
}
