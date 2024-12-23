namespace Api.Orchestration.Athletes.Compare;

public record CompareAthletesStat
{
    public required int ActualTotal { get; init; }
    public required int? GoalTotal { get; init; }
    public required string RaceSeriesTypeName { get; init; }
}
