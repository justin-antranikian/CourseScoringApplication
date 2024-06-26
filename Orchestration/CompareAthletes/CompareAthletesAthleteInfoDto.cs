﻿namespace Orchestration.CompareAthletes;

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

    public static CompareAthletesAthleteInfoDto GetCompareAthletesAthleteInfoDto(Athlete athlete, List<Course> courses, List<CompareAthletesResult> resultsForAthlete)
    {
        var locationInfoWithRank = new LocationInfoWithRank(athlete);
        var coursesForAthlete = GetCoursesForAthlete(courses, resultsForAthlete);

        static CompareAthletesStat GetCompareAthletesStat(IGrouping<RaceSeriesType, Course> raceSeriesTypeGrouping)
        {
            var raceSeriesTypeName = raceSeriesTypeGrouping.Key.ToFriendlyText();
            var distance = raceSeriesTypeGrouping.Sum(oo => oo.Distance);
            return new CompareAthletesStat(raceSeriesTypeName, raceSeriesTypeGrouping.Count(), distance);
        }

        var stats = coursesForAthlete
                        .GroupBy(oo => oo.Race.RaceSeries.RaceSeriesType)
                        .Select(GetCompareAthletesStat)
                        .OrderBy(oo => oo.RaceSeriesTypeName)
                        .ToList();

        return new CompareAthletesAthleteInfoDto
        (
            locationInfoWithRank,
            athlete.FullName,
            athlete.DateOfBirth,
            athlete.Gender,
            resultsForAthlete,
            stats
        );
    }
}

public class CompareAthletesAthleteInfoDto
{
    public LocationInfoWithRank LocationInfoWithRank { get; }

    public string FullName { get; }

    public int Age { get; }

    public string GenderAbbreviated { get; }

    public List<CompareAthletesResult> Results { get; }

    public List<CompareAthletesStat> Stats { get; }

    public CompareAthletesAthleteInfoDto
    (
        LocationInfoWithRank locationInfoWithRank,
        string fullName,
        DateTime dateOfBirth,
        Gender gender,
        List<CompareAthletesResult> results,
        List<CompareAthletesStat> stats
    )
    {
        LocationInfoWithRank = locationInfoWithRank;
        FullName = fullName;
        GenderAbbreviated = gender.ToAbbreviation();
        Age = DateTimeHelper.GetCurrentAge(dateOfBirth);
        Results = results;
        Stats = stats;
    }
}
