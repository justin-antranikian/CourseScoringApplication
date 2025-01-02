namespace Api.Orchestration.Races.Search;

public record RaceSearchResultDto
{
    public required int Id { get; init; }

    public required LocationInfoWithRank LocationInfoWithRank { get; init; }
    public required string Name { get; init; }
    public required string RaceKickOffDate { get; init; }
    public required string RaceSeriesType { get; init; }
    public required int UpcomingRaceId { get; init; }

    public required List<DisplayNameWithIdDto> Courses { get; init; }
}
