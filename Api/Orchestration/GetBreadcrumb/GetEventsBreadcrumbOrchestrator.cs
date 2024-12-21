using Api.DataModels;
using Api.Orchestration.GetBreadcrumb.EventsBreadcrumbCreators;

namespace Api.Orchestration.GetBreadcrumb;

public class GetEventsBreadcrumbOrchestrator(ScoringDbContext scoringDbContext) : GetBreadcrumbOrchestratorBase
{
    static GetEventsBreadcrumbOrchestrator()
    {
        _stateAreaOrCityNavigationLevels = [BreadcrumbNavigationLevel.State, BreadcrumbNavigationLevel.Area, BreadcrumbNavigationLevel.City];
    }

    /// <summary>
    /// Types include State, Area, and City. Only one instance of this list is needed for the application.
    /// </summary>
    private static readonly BreadcrumbNavigationLevel[] _stateAreaOrCityNavigationLevels;

    public async Task<EventsBreadcrumbResultDto> GetResult(BreadcrumbRequestDto breadcrumbRequestDto)
    {
        if (IsStateAreaOrCity(breadcrumbRequestDto))
        {
            var locationInfoWithUrl = GetLocationInfoWithUrl(breadcrumbRequestDto);
            return new EventsBreadcrumbResultDto(locationInfoWithUrl);
        }

        var breadcrumbCreator = GetBreadcrumbCreatorForNonLocationTypes(breadcrumbRequestDto);
        var breadcrumbResult = await breadcrumbCreator.GetBreadcrumbResult(breadcrumbRequestDto, scoringDbContext);
        return breadcrumbResult;
    }

    private static bool IsStateAreaOrCity(BreadcrumbRequestDto breadcrumbRequestDto)
    {
        var navigationLevel = breadcrumbRequestDto.BreadcrumbNavigationLevel;
        return _stateAreaOrCityNavigationLevels.Contains(navigationLevel);
    }

    private static EventsBreadcrumbCreatorBase GetBreadcrumbCreatorForNonLocationTypes(BreadcrumbRequestDto breadcrumbRequestDto)
    {
        return breadcrumbRequestDto.BreadcrumbNavigationLevel switch
        {
            BreadcrumbNavigationLevel.ArpOrRaceSeriesDashboard => new RaceSeriesBreadcrumbCreator(),
            BreadcrumbNavigationLevel.RaceLeaderboard => new RaceBreadcrumbCreator(),
            BreadcrumbNavigationLevel.CourseLeaderboard => new CourseBreadcrumbCreator(),
            BreadcrumbNavigationLevel.Irp => new IrpBreadcrumbCreator(),
            _ => throw new NotImplementedException("State, Area, and City types are not implemented in this block.")
        };
    }
}

