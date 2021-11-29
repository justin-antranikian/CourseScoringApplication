using Core;
using DataModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orchestration.GetCourseStatistics
{
	public class GetCourseStatisticsOrchestrator
	{
		private readonly ScoringDbContext _scoringDbContext;

		public GetCourseStatisticsOrchestrator(ScoringDbContext scoringDbContext)
		{
			_scoringDbContext = scoringDbContext;
		}

		public async Task<List<CourseStatisticDto>> GetStatisticDto(int athleteCourseId)
		{
			var athleteCourse = await _scoringDbContext.AthleteCourses.Include(oo => oo.Course).SingleAsync(oo => oo.Id == athleteCourseId);
			var courseStatistics = await _scoringDbContext.CourseStatistics.Where(oo => oo.CourseId == athleteCourse.CourseId).ToListAsync();

			PaceWithTime GetPaceWithTime(int timeInMilleseconds)
			{
				return athleteCourse.Course.GetPaceWithTime(timeInMilleseconds);
			}

			var stats = courseStatistics.Select(oo =>
			{
				return new CourseStatisticDto
				(
					oo.BracketId,
					GetPaceWithTime(oo.AverageTotalTimeInMilleseconds),
					GetPaceWithTime(oo.FastestTimeInMilleseconds),
					GetPaceWithTime(oo.SlowestTimeInMilleseconds)
				);
			});

			return stats.ToList();
		}
	}
}
