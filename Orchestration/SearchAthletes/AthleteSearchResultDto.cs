namespace Orchestration.AthletesSearch;

public static class AthleteSearchResultDtoMapper
{
    public static List<AthleteSearchResultDto> GetAthleteSearchResultDto(IEnumerable<Athlete> athletes)
    {
        return athletes.Select(GetAthleteSearchResultDto).ToList();
    }

    public static AthleteSearchResultDto GetAthleteSearchResultDto(Athlete athlete)
    {
        return new
        (
            athlete.Id,
            athlete.FullName,
            DateTimeHelper.GetCurrentAge(athlete.DateOfBirth),
            athlete.Gender.ToAbbreviation(),
            new LocationInfoWithRank(athlete),
            athlete.GetTags()
        );
    }
}

public record AthleteSearchResultDto
(
    int Id,
    string FullName,
    int Age,
    string GenderAbbreviated,
    LocationInfoWithRank LocationInfoWithRank,
    List<string> Tags
);
