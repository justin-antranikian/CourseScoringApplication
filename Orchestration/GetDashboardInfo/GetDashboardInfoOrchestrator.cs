using System.Threading;

namespace Orchestration.GetDashboardInfo;

public partial class GetDashboardInfoOrchestrator
{
    private readonly ScoringDbContext _scoringDbContext;

    public GetDashboardInfoOrchestrator(ScoringDbContext scoringDbContext)
    {
        _scoringDbContext = scoringDbContext;
    }

    public DashboardInfoResponseDto GetResult(DashboardInfoRequestDto request)
    {
        var locations = GetLocations(request);
        if (request.DashboardInfoLocationType == DashboardInfoLocationType.All)
        {
            var states = DashboardInfoHelper.GetStates(locations.GroupBy(oo => oo.State));
            return new DashboardInfoResponseDto(states);
        }

        var filterPredicate = GetFilterPredicate(request);
        var location = LocationHelper.Find(filterPredicate);
        return GetDashboardInfoCreator(request, location).GetResult(locations);
    }

    private List<Location> GetLocations(DashboardInfoRequestDto requestDto)
    {
        var isEvents = requestDto.DashboardInfoType == DashboardInfoType.Events;
        return isEvents ? GetLocationsByEventsFromCache(_scoringDbContext) : GetLocationsByAthletesFromCache(_scoringDbContext);
    }

    private Func<Location, bool> GetFilterPredicate(DashboardInfoRequestDto request)
    {
        var locationUrl = request.LocationTermUrlFriendly;
        return request.DashboardInfoLocationType switch
        {
            DashboardInfoLocationType.State => (Location oo) => oo.StateUrlFriendly == locationUrl,
            DashboardInfoLocationType.Area => (Location oo) => oo.AreaUrlFriendly == locationUrl,
            DashboardInfoLocationType.City => (Location oo) => oo.CityUrlFriendly == locationUrl,
            _ => throw new NotImplementedException("Only State, Area, and City types are implemented in this block.")
        };
    }

    private static DashboardInfoCreatorBase GetDashboardInfoCreator(DashboardInfoRequestDto request, Location location)
    {
        return request.DashboardInfoLocationType switch
        {
            DashboardInfoLocationType.State => new DashboardInfoByStateResultCreator(location),
            DashboardInfoLocationType.Area => new DashboardInfoByAreaResultCreator(location),
            DashboardInfoLocationType.City => new DashboardInfoByCityResultCreator(location),
            _ => throw new NotImplementedException("Only State, Area, and City types are implemented in this block.")
        };
    }
}
