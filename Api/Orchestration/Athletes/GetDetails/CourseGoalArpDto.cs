using Api.DataModels;

namespace Api.Orchestration.Athletes.GetDetails;

public static class CourseGoalArpDtoMapper
{
    public static CourseGoalArpDto GetCourseGoalArpDto(Course course)
    {
        var race = course.Race;
        var raceSeries = race.RaceSeries!;

        return new CourseGoalArpDto
        {
            CourseId = course.Id,
            CourseName = course.Name,
            RaceId = race.Id,
            RaceName = race.Name,
            RaceSeriesCity = raceSeries.CityLocation!.Name,
            RaceSeriesState = raceSeries.StateLocation!.Name,
            RaceSeriesDescription = raceSeries.Description
        };
    }
}

public record CourseGoalArpDto
{
    public required int CourseId { get; init; }
    public required string CourseName { get; init; }
    public required int RaceId { get; init; }
    public required string RaceName { get; init; }
    public required string RaceSeriesCity { get; init; }
    public required string RaceSeriesState { get; init; }
    public required string RaceSeriesDescription { get; init; }
}
