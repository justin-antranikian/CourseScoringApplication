using Core;

namespace Orchestration.GetCourseInfo
{
	public record CourseInfoDto
	(
		string TimeZoneAbbreviated,
		string RaceName,
		string RaceSeriesCity,
		string RaceSeriesState,
		string RaceSeriesDescription,
		string CourseDate,
		string CourseTime,
		string CourseName,
		double CourseDistance,
		RaceSeriesType RaceSeriesType
	);
}
