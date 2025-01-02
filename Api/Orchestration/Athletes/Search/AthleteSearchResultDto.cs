namespace Api.Orchestration.Athletes.Search;

public record AthleteSearchResultDto
{
    public required int Id { get; init; }

    public required int Age { get; init; }
    public required string FullName { get; init; }
    public required string GenderAbbreviated { get; init; }
    public required LocationInfoWithRank LocationInfoWithRank { get; init; }
}
