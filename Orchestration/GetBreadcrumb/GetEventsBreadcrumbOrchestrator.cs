using Orchestration.GetBreadcrumb.EventsBreadcrumbCreators;
using System.Threading.Tasks;

namespace Orchestration.GetBreadcrumb;

public class GetEventsBreadcrumbOrchestrator : GetBreadcrumbOrchestratorBase
{
    static GetEventsBreadcrumbOrchestrator()
    {
        _stateAreaOrCityNavigationLevels = new[] { BreadcrumbNavigationLevel.State, BreadcrumbNavigationLevel.Area, BreadcrumbNavigationLevel.City };
    }

    /// <summary>
    /// Types include State, Area, and City. Only one instance of this list is needed for the application.
    /// </summary>
    private static readonly BreadcrumbNavigationLevel[] _stateAreaOrCityNavigationLevels;

    private readonly ScoringDbContext _scoringDbContext;

    public GetEventsBreadcrumbOrchestrator(ScoringDbContext scoringDbContext)
    {
        _scoringDbContext = scoringDbContext;
    }

    public async Task<EventsBreadcrumbResultDto> GetResult(BreadcrumbRequestDto breadcrumbRequestDto)
    {
        if (IsStateAreaOrCity(breadcrumbRequestDto))
        {
            var locationInfoWithUrl = GetLocationInfoWithUrl(breadcrumbRequestDto);
            return new EventsBreadcrumbResultDto(locationInfoWithUrl);
        }

        var breadcrumbCreator = GetBreadcrumbCreatorForNonLocationTypes(breadcrumbRequestDto);
        var breadcrumbResult = await breadcrumbCreator.GetBreadcrumbResult(breadcrumbRequestDto, _scoringDbContext);
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

