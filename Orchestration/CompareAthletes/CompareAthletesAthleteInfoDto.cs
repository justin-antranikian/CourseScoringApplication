namespace Orchestration.CompareAthletes;

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
                        .GroupBy(oo => oo.Race.RaceSeries.RaceSeriesType)
                        .Select(GetCompareAthletesStat)
                        .OrderBy(oo => oo.RaceSeriesTypeName)
                        .ToList();

        return new CompareAthletesAthleteInfoDto
        (
            athlete.Id,
            locationInfoWithRank,
            athlete.FullName,
            athlete.DateOfBirth,
            athlete.Gender,
            stats
        );
    }
}

public class CompareAthletesAthleteInfoDto
{
    public int Id { get; set; }
    public LocationInfoWithRank LocationInfoWithRank { get; }
    public string FullName { get; }
    public int Age { get; }
    public string GenderAbbreviated { get; }
    public List<CompareAthletesResult> Results { get; }
    public List<CompareAthletesStat> Stats { get; }

    public CompareAthletesAthleteInfoDto
    (
        int id,
        LocationInfoWithRank locationInfoWithRank,
        string fullName,
        DateTime dateOfBirth,
        Gender gender,
        List<CompareAthletesStat> stats
    )
    {
        Id = id;
        LocationInfoWithRank = locationInfoWithRank;
        FullName = fullName;
        GenderAbbreviated = gender.ToAbbreviation();
        Age = DateTimeHelper.GetCurrentAge(dateOfBirth);
        Stats = stats;
    }
}
