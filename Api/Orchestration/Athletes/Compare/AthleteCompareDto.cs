namespace Api.Orchestration.Athletes.Compare;

public record AthleteCompareDto
{
    public required int Id { get; set; }

    public required int Age { get; init; }
    public required string GenderAbbreviated { get; init; }
    public required string FullName { get; init; }
    public required LocationInfoWithRank LocationInfoWithRank { get; init; }
    public required List<AthleteCompareStatDto> Stats { get; init; }
}
