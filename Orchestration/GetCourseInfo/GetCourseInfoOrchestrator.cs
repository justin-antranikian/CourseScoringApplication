using Core;
using DataModels;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Orchestration.GetCourseInfo
{
	public class GetCourseInfoOrchestrator
	{
		private readonly ScoringDbContext _scoringDbContext;

		public GetCourseInfoOrchestrator(ScoringDbContext scoringDbContext)
		{
			_scoringDbContext = scoringDbContext;
		}

		public async Task<CourseInfoDto> GetCourseInfo(int courseId)
		{
			var course = await GetCourse(courseId);
			var timeZoneAbbreviated = course.Race.TimeZoneId.ToAbbreviation();
			var (courseDate, courseTime) = DateTimeHelper.GetFormattedFields(course.StartDate);

			return new CourseInfoDto
			(
				timeZoneAbbreviated,
				course.Race.Name,
				course.Race.RaceSeries.City,
				course.Race.RaceSeries.State,
				course.Race.RaceSeries.Description,
				courseDate,
				courseTime,
				course.Name,
				course.Distance,
				course.Race.RaceSeries.RaceSeriesType
			);
		}

		private async Task<Course> GetCourse(int courseId)
		{
			return await _scoringDbContext.Courses
							.Include(oo => oo.Race)
							.ThenInclude(oo => oo.RaceSeries)
							.SingleAsync(oo => oo.Id == courseId);
		}
	}
}
