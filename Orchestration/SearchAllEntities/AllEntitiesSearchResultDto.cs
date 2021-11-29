using Orchestration.AthletesSearch;
using Orchestration.GetRaceSeriesSearch;
using System.Collections.Generic;

namespace Orchestration.GetSearchAllEntitiesSearch
{
	public record AllEntitiesSearchResultDto(List<AthleteSearchResultDto> Athletes, List<EventSearchResultDto> RaceSeries);
}
