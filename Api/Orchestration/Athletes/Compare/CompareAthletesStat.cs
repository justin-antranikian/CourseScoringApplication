namespace Api.Orchestration.Athletes.Compare;

public record CompareAthletesStat
(
    string RaceSeriesTypeName,
    int ActualTotal,
    int? GoalTotal
);
