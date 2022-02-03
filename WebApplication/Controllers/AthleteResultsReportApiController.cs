using DataModels;
using Microsoft.AspNetCore.Mvc;
using Orchestration.Reports.AthleteResultsReport;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication.Controllers;

[Route("[controller]")]
public class AthleteResultsReportApiController : ControllerBase
{
	private readonly ScoringDbContext _scoringDbContext;

	public AthleteResultsReportApiController(ScoringDbContext scoringDbContext)
	{
		_scoringDbContext = scoringDbContext;
	}

	[HttpGet]
	public async Task<List<AthleteReportDto>> Get()
	{
		var orchestrator = new AthleteResultsReportOrchestrator(_scoringDbContext);
		var defaultDate = new DateTime(2020, 1, 1);
		return await orchestrator.GetAthleteReportDtos(defaultDate);
	}
}
