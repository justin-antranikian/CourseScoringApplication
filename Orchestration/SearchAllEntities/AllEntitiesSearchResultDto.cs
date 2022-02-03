using Orchestration.AthletesSearch;
using Orchestration.GetRaceSeriesSearch;

namespace Orchestration.GetSearchAllEntitiesSearch;

public record AllEntitiesSearchResultDto(List<AthleteSearchResultDto> Athletes, List<EventSearchResultDto> RaceSeries);
