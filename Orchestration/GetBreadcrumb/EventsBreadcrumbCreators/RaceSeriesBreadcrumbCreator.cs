using DataModels;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Orchestration.GetBreadcrumb.EventsBreadcrumbCreators;

internal class RaceSeriesBreadcrumbCreator : EventsBreadcrumbCreatorBase
{
	public override async Task<EventsBreadcrumbResultDto> GetBreadcrumbResult(BreadcrumbRequestDto breadcrumbRequestDto, ScoringDbContext scoringDbContext)
	{
		var raceSeriesId = int.Parse(breadcrumbRequestDto.SearchTerm);
		var raceSeries = await scoringDbContext.RaceSeries.SingleAsync(oo => oo.Id == raceSeriesId);

		var raceSeriesDisplay = GetRaceSeriesDisplayName(raceSeries);
		var locationInfoWithUrl = new LocationInfoWithUrl(raceSeries);
		return new EventsBreadcrumbResultDto(locationInfoWithUrl, raceSeriesDisplay);
	}
}
