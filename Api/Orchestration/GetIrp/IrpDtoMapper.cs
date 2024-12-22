using Api.DataModels;

namespace Api.Orchestration.GetIrp;

public static class IrpDtoMapper
{
    public static IrpDto GetIrpDto(AthleteCourse athleteCourse, Course course, PaceWithTime paceWithTime, List<IrpResultByBracketDto> bracketResults, List<IrpResultByIntervalDto> intervalResults)
    {
        var athlete = athleteCourse.Athlete;
        var race = course.Race;

        var trainingList = athleteCourse.GetTrainingList();
        var finishTime = intervalResults.Single(oo => oo.IsFullCourse).CrossingTime;

        var irpDto = new IrpDto
        {
            AthleteId = athlete.Id,
            Bib = athleteCourse.Bib,
            BracketResults = bracketResults,
            CourseGoalDescription = athleteCourse.CourseGoalDescription,
            FinishTime = finishTime,
            FirstName = athlete.FirstName,
            FullName = athlete.FullName,
            GenderAbbreviated = athlete.Gender.ToAbbreviation(),
            IntervalResults = intervalResults,
            LocationInfoWithRank = new LocationInfoWithRank(athlete),
            PaceWithTimeCumulative = paceWithTime,
            PersonalGoalDescription = athleteCourse.PersonalGoalDescription,
            RaceAge = DateTimeHelper.GetRaceAge(athlete.DateOfBirth, course.StartDate),
            Tags = athlete.GetTags(),
            TimeZoneAbbreviated = race.TimeZoneId.ToAbbreviation(),
            TrainingList = trainingList
        };

        return irpDto;
    }
}
