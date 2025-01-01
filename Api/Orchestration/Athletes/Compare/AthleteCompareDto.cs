using Api.DataModels;

namespace Api.Orchestration.Athletes.Compare;

public static class CompareAthletesAthleteInfoDtoMapper
{
    public static AthleteCompareDto GetCompareAthletesAthleteInfoDto(Athlete athlete, List<Course> courses, List<Result> resultsForAthlete, List<AthleteRaceSeriesGoal> goals)
    {
        var coursesForAthlete = GetCoursesForAthlete(courses, resultsForAthlete);

        AthleteCompareStatDto GetCompareAthletesStat(IGrouping<RaceSeriesType, Course> raceSeriesTypeGrouping)
        {
            var raceSeriesType = raceSeriesTypeGrouping.Key;
            var goal = goals.SingleOrDefault(oo => oo.RaceSeriesType == raceSeriesType);

            return new AthleteCompareStatDto
            {
                ActualTotal = raceSeriesTypeGrouping.Count(),
                GoalTotal = goal?.TotalEvents,
                RaceSeriesType = raceSeriesType.ToString(),
            };
        }

        var stats = coursesForAthlete
            .GroupBy(oo => oo.Race.RaceSeries.RaceSeriesType)
            .Select(GetCompareAthletesStat)
            .ToList();

        return new AthleteCompareDto
        {
            Id = athlete.Id,
            Age = DateTimeHelper.GetCurrentAge(athlete.DateOfBirth),
            FullName = athlete.FullName,
            GenderAbbreviated = athlete.GetGenderFormatted(),
            LocationInfoWithRank = athlete.ToLocationInfoWithRank(),
            Stats = stats
        };
    }

    private static List<Course> GetCoursesForAthlete(List<Course> courses, List<Result> resultsForAthlete)
    {
        var filter = resultsForAthlete.Join(courses,
                        result => result.CourseId,
                        course => course.Id,
                        (_, course) => course);

        return filter.ToList();
    }
}

public record AthleteCompareDto
{
    public required int Id { get; set; }
    public required int Age { get; init; }
    public required string GenderAbbreviated { get; init; }
    public required string FullName { get; init; }
    public required LocationInfoWithRank LocationInfoWithRank { get; init; }
    public required List<AthleteCompareStatDto> Stats { get; init; }
}
