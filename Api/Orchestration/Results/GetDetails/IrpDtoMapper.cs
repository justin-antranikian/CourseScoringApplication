using Api.DataModels;

namespace Api.Orchestration.Results.GetDetails;

public static class IrpDtoMapper
{
    public static IrpDto GetIrpDto(AthleteCourse athleteCourse, Course course, PaceWithTime paceWithTime, List<IrpResultByBracketDto> bracketResults, List<IrpResultByIntervalDto> intervalResults)
    {
        var athlete = athleteCourse.Athlete;
        var race = course.Race;
        var trainingList = athleteCourse.AthleteCourseTrainings.Select(oo => oo.Description).ToList();
        var finishTime = intervalResults.Single(oo => oo.IsFullCourse).CrossingTime;

        return new IrpDto
        {
            AthleteId = athlete.Id,
            Bib = athleteCourse.Bib,
            BracketResults = bracketResults,
            CourseId = course.Id,
            CourseName = course.Name,
            CourseGoalDescription = athleteCourse.CourseGoalDescription,
            FinishTime = finishTime,
            FirstName = athlete.FirstName,
            FullName = athlete.FullName,
            GenderAbbreviated = athlete.Gender.ToAbbreviation(),
            IntervalResults = intervalResults,
            LocationInfoWithRank = athlete.ToLocationInfoWithRank(),
            PaceWithTimeCumulative = paceWithTime,
            PersonalGoalDescription = athleteCourse.PersonalGoalDescription,
            RaceAge = DateTimeHelper.GetRaceAge(athlete.DateOfBirth, course.StartDate),
            RaceId = race.Id,
            RaceName = race.Name,
            RaceSeriesLocationInfoWithRank = race.RaceSeries!.ToLocationInfoWithRank(),
            Tags = athlete.GetTags(),
            TimeZoneAbbreviated = race.TimeZoneId.ToAbbreviation(),
            TrainingList = trainingList
        };
    }
}
