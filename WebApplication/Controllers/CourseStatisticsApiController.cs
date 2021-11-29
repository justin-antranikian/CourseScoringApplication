using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DataModels;
using Orchestration.GetCourseStatistics;
using System.Collections.Generic;

namespace WebApplicationSandbox.Controllers
{
	[Route("[controller]")]
	public class CourseStatisticsApiController : ControllerBase
	{
		private readonly ScoringDbContext _scoringDbContext;

		public CourseStatisticsApiController(ScoringDbContext scoringDbContext)
		{
			_scoringDbContext = scoringDbContext;
		}

		[HttpGet]
		[Route("{athleteCourseId:int}")]
		public async Task<List<CourseStatisticDto>> Get(int athleteCourseId)
		{
			var orchestrator = new GetCourseStatisticsOrchestrator(_scoringDbContext);
			return await orchestrator.GetStatisticDto(athleteCourseId);
		}
	}
}
