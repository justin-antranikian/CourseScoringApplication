using Api.DataModels;

namespace Api.Orchestration.Races.Search;

public record EventSearchResultDto
{
    public required int Id { get; init; }
    public required LocationInfoWithRank LocationInfoWithRank { get; init; }
    public required string Name { get; init; }
    public required string RaceSeriesTypeName { get; init; }
    public required int UpcomingRaceId { get; init; }

    public required List<DisplayNameWithIdDto> Courses { get; init; }
}
