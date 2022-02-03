using DataModels;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Orchestration.GetBreadcrumb.EventsBreadcrumbCreators;

internal class RaceBreadcrumbCreator : EventsBreadcrumbCreatorBase
{
	public override async Task<EventsBreadcrumbResultDto> GetBreadcrumbResult(BreadcrumbRequestDto breadcrumbRequestDto, ScoringDbContext scoringDbContext)
	{
		var raceId = int.Parse(breadcrumbRequestDto.SearchTerm);
		var race = await scoringDbContext.Races.Include(oo => oo.RaceSeries).SingleAsync(oo => oo.Id == raceId);

		var raceSeries = race.RaceSeries;
		var raceDisplay = GetRaceDisplayName(race);
		var raceSeriesDisplay = GetRaceSeriesDisplayName(raceSeries);
		var locationInfoWithUrl = new LocationInfoWithUrl(raceSeries);
		return new EventsBreadcrumbResultDto(locationInfoWithUrl, raceSeriesDisplay, raceDisplay);
	}
}
