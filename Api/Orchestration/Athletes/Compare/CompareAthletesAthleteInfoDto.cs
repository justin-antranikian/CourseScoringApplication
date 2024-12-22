using Api.DataModels;

namespace Api.Orchestration.Athletes.Compare;

public static class CompareAthletesAthleteInfoDtoMapper
{
    private static List<Course> GetCoursesForAthlete(List<Course> courses, List<CompareAthletesResult> resultsForAthlete)
    {
        var filter = resultsForAthlete.Join(courses,
                        result => result.CourseId,
                        course => course.Id,
                        (_, course) => course);

        return filter.ToList();
    }

    public static CompareAthletesAthleteInfoDto GetCompareAthletesAthleteInfoDto(Athlete athlete, List<Course> courses, List<CompareAthletesResult> resultsForAthlete, List<AthleteRaceSeriesGoal> goals)
    {
        var locationInfoWithRank = new LocationInfoWithRank(athlete);
        var coursesForAthlete = GetCoursesForAthlete(courses, resultsForAthlete);

        CompareAthletesStat GetCompareAthletesStat(IGrouping<RaceSeriesType, Course> raceSeriesTypeGrouping)
        {
            var raceSeriesType = raceSeriesTypeGrouping.Key;
            var raceSeriesTypeName = raceSeriesType.ToFriendlyText();
            var goal = goals.SingleOrDefault(oo => oo.RaceSeriesType == raceSeriesType);
            return new CompareAthletesStat(raceSeriesTypeName, raceSeriesTypeGrouping.Count(), goal?.TotalEvents);
        }

        var stats = coursesForAthlete
                        .GroupBy(oo => oo.Race!.RaceSeries!.RaceSeriesType)
                        .Select(GetCompareAthletesStat)
                        .OrderBy(oo => oo.RaceSeriesTypeName)
                        .ToList();

        return new CompareAthletesAthleteInfoDto
        {
            Id = athlete.Id,
            Age = DateTimeHelper.GetCurrentAge(athlete.DateOfBirth),
            FullName = athlete.FullName,
            GenderAbbreviated = athlete.Gender.ToAbbreviation(),
            LocationInfoWithRank = locationInfoWithRank,
            Stats = stats
        };
    }
}

public record CompareAthletesAthleteInfoDto
{
    public required int Id { get; set; }
    public required int Age { get; init; }
    public required string GenderAbbreviated { get; init; }
    public required string FullName { get; init; }
    public required LocationInfoWithRank LocationInfoWithRank { get; init; }
    public required List<CompareAthletesStat> Stats { get; init; }
}
