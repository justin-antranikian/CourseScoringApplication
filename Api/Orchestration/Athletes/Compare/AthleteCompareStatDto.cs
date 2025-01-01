namespace Api.Orchestration.Athletes.Compare;

public record AthleteCompareStatDto
{
    public required int ActualTotal { get; init; }
    public required int? GoalTotal { get; init; }
    public required string RaceSeriesType { get; init; }
}
