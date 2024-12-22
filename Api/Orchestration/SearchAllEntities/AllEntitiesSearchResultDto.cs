using Api.Orchestration.Athletes.Search;
using Api.Orchestration.Races.SearchEvents;

namespace Api.Orchestration.SearchAllEntities;

public record AllEntitiesSearchResultDto(List<AthleteSearchResultDto> Athletes, List<EventSearchResultDto> RaceSeries);
