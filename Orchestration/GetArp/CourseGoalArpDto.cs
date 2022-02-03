namespace Orchestration.GetArp;

public static class CourseGoalArpDtoMapper
{
	public static CourseGoalArpDto GetCourseGoalArpDto(Course course)
	{
		var race = course.Race;
		var raceSeries = race.RaceSeries;

		return new
		(
			course.Id,
			course.Name,
			race.Id,
			race.Name,
			raceSeries.State,
			raceSeries.City,
			raceSeries.Description
		);
	}
}

public record CourseGoalArpDto
(
	int CourseId,
	string CourseName,
	int RaceId,
	string RaceName,
	string RaceSeriesState,
	string RaceSeriesCity,
	string RaceSeriesDescription
);
