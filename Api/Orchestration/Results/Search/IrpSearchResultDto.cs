using Api.DataModels;

namespace Api.Orchestration.Results.Search;

public static class IrpSearchResultDtoMapper
{
    public static List<IrpSearchResultDto> GetIrpSearchResultDto(List<AthleteCourse> athleteCourses)
    {
        return athleteCourses.Select(GetIrpSearchResultDto).ToList();
    }

    public static IrpSearchResultDto GetIrpSearchResultDto(AthleteCourse athleteCourse)
    {
        var athlete = athleteCourse.Athlete;
        var course = athleteCourse.Course;

        return new IrpSearchResultDto
        {
            AthleteCourseId = athlete.Id,
            Bib = athleteCourse.Bib,
            City = athlete.City,
            CourseName = course.Name,
            FullName = athlete.FullName,
            GenderAbbreviated = athlete.Gender.ToAbbreviation(),
            RaceAge = DateTimeHelper.GetRaceAge(athlete.DateOfBirth, course.StartDate),
            State = athlete.State,
        };
    }
}

public record IrpSearchResultDto
{
    public required int AthleteCourseId { get; init; }
    public required string Bib { get; init; }
    public required string City { get; init; }
    public required string CourseName { get; init; }
    public required string FullName { get; init; }
    public required string GenderAbbreviated { get; init; }
    public required int RaceAge { get; init; }
    public required string State { get; init; }
}
