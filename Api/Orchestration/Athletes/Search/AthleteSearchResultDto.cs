using Api.DataModels;

namespace Api.Orchestration.Athletes.Search;

public static class AthleteSearchResultDtoMapper
{
    public static AthleteSearchResultDto GetAthleteSearchResultDto(Athlete athlete)
    {
        return new AthleteSearchResultDto
        {
            Id = athlete.Id,
            Age = DateTimeHelper.GetCurrentAge(athlete.DateOfBirth),
            FullName = athlete.FullName,
            GenderAbbreviated = athlete.Gender.ToAbbreviation(),
            LocationInfoWithRank = athlete.ToLocationInfoWithRank(),
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
