using Api.DataModels;

namespace Api.Orchestration.Results.GetDetails;

public static class IrpDtoMapper
{
    public static IrpDto GetIrpDto(AthleteCourse athleteCourse, Course course, PaceWithTime paceWithTime, List<IrpResultByBracketDto> bracketResults, List<IrpResultByIntervalDto> intervalResults)
    {
        var athlete = athleteCourse.Athlete;
        var race = course.Race;

        return new IrpDto
        {
            AthleteId = athlete.Id,
            Bib = athleteCourse.Bib,
            BracketResults = bracketResults,
            CourseId = course.Id,
            CourseName = course.Name,
            CourseGoalDescription = athleteCourse.CourseGoalDescription,
            FinishTime = intervalResults.Single(oo => oo.IsFullCourse).CrossingTime,
            FirstName = athlete.FirstName,
            FullName = athlete.FullName,
            GenderAbbreviated = athlete.GetGenderFormatted(),
            IntervalResults = intervalResults,
            LocationInfoWithRank = athlete.ToLocationInfoWithRank(),
            PaceWithTimeCumulative = paceWithTime,
            PersonalGoalDescription = athleteCourse.PersonalGoalDescription,
            RaceAge = DateTimeHelper.GetRaceAge(athlete.DateOfBirth, course.StartDate),
            RaceId = race.Id,
            RaceName = race.Name,
            RaceSeriesLocationInfoWithRank = race.RaceSeries.ToLocationInfoWithRank(),
            Tags = athlete.AthleteRaceSeriesGoals.Select(oo => oo.RaceSeriesType.ToString()).ToList(),
            TimeZoneAbbreviated = ToTimeZoneAbbreviation(race.TimeZoneId),
            TrainingList = athleteCourse.AthleteCourseTrainings.Select(oo => oo.Description).ToList()
        };
    }

    private static string ToTimeZoneAbbreviation(string timeZoneId)
    {
        return timeZoneId switch
        {
            "Pacific Standard Time" => "PST",
            "Mountain Standard Time" => "MST",
            "Central Standard Time" => "CST",
            "Eastern Standard Time" => "EST",
            _ => throw new NotImplementedException()
        };
    }
}
