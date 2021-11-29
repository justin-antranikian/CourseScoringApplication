using DataModels;
using Microsoft.AspNetCore.Mvc;
using Orchestration.Reports.RacesByMonthReport;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
	[Route("[controller]")]
	public class RacesByMonthReportApiController : ControllerBase
	{
		private readonly ScoringDbContext _scoringDbContext;

		public RacesByMonthReportApiController(ScoringDbContext scoringDbContext)
		{
			_scoringDbContext = scoringDbContext;
		}

		[HttpGet]
		public async Task<List<ReportByMonthDto>> Get()
		{
			var orchestrator = new RacesByMonthReportOrchestrator(_scoringDbContext);
			var defaultDate = new DateTime(2020, 1, 1);
			return await orchestrator.GetReportByMonthDtos(defaultDate);
		}
	}
}
