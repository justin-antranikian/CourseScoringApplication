using Api.DataModels;

namespace Api.Orchestration.Results.GetDetails;

public static class IrpDtoMapper
{
    public static IrpDto GetIrpDto(Athlete athlete, AthleteCourse athleteCourse, Course course, PaceWithTime paceWithTime, List<IrpResultByBracketDto> bracketResults, List<IrpResultByIntervalDto> intervalResults)
    {
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

/// <summary>
/// Abbreviation stands for individual results page.
/// </summary>
public class IrpDto
{
    public required int AthleteId { get; init; }
    public required string Bib { get; init; }
    public required List<IrpResultByBracketDto> BracketResults { get; init; }
    public required int CourseId { get; init; }
    public required string CourseName { get; init; }
    public required string CourseGoalDescription { get; init; }
    public required string? FinishTime { get; init; }
    public required string FirstName { get; init; }
    public required string FullName { get; init; }
    public required string GenderAbbreviated { get; init; }
    public required List<IrpResultByIntervalDto> IntervalResults { get; init; }
    public required LocationInfoWithRank LocationInfoWithRank { get; init; }
    public required PaceWithTime PaceWithTimeCumulative { get; init; }
    public required string PersonalGoalDescription { get; init; }
    public required int RaceAge { get; init; }
    public required List<string> Tags { get; init; }
    public required string TimeZoneAbbreviated { get; init; }
    public required List<string> TrainingList { get; init; }
}
