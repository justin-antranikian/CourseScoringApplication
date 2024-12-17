﻿namespace Orchestration.CompareAthletes;

public record CompareAthletesStat
(
    string RaceSeriesTypeName,
    int ActualTotal,
    int? GoalTotal
);
