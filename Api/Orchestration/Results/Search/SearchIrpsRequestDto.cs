﻿namespace Api.Orchestration.Results.Search;

public record SearchIrpsRequestDto
{
    public int? RaceId { get; init; }
    public int? CourseId { get; init; }
    public SearchOnField SearchOn { get; init; }
    public string? SearchTerm { get; init; }
}
