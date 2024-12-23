﻿using Api.DataModels;

namespace Api.Orchestration.Athletes.Search;

public static class AthleteSearchResultDtoMapper
{
    public static List<AthleteSearchResultDto> GetAthleteSearchResultDto(IEnumerable<Athlete> athletes)
    {
        return athletes.Select(GetAthleteSearchResultDto).ToList();
    }

    public static AthleteSearchResultDto GetAthleteSearchResultDto(Athlete athlete)
    {
        return new AthleteSearchResultDto
        {
            Id = athlete.Id,
            Age = DateTimeHelper.GetCurrentAge(athlete.DateOfBirth),
            FullName = athlete.FullName,
            GenderAbbreviated = athlete.Gender.ToAbbreviation(),
            LocationInfoWithRank = new LocationInfoWithRank(athlete),
            Tags = athlete.GetTags()
        };
    }
}

public record AthleteSearchResultDto
{
    public required int Id { get; init; }
    public required int Age { get; init; }
    public required string FullName { get; init; }
    public required string GenderAbbreviated { get; init; }
    public required LocationInfoWithRank LocationInfoWithRank { get; init; }
    public required List<string> Tags { get; init; }
}
