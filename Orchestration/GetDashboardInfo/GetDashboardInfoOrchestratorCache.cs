namespace Orchestration.GetDashboardInfo;

/// <summary>
/// Contains logic for caching for this the GetDashboardInfoOrchestrator class.
/// </summary>
public partial class GetDashboardInfoOrchestrator
{
    private static readonly object _eventsLock = new();
    private static readonly object _athletesLock = new();

    private static List<Location> _locationsByAthletes;
    private static List<Location> _locationsByEvents;

    private static List<Location> GetLocationsByAthletesFromCache(ScoringDbContext scoringDbContext)
    {
        if (_locationsByAthletes != null)
        {
            return _locationsByAthletes;
        }

        lock (_athletesLock)
        {
            _locationsByAthletes ??= scoringDbContext.Athletes.Select(oo => new Location(oo.State, oo.Area, oo.City)).ToList();
            return _locationsByAthletes;
        }
    }

    private static List<Location> GetLocationsByEventsFromCache(ScoringDbContext scoringDbContext)
    {
        if (_locationsByEvents != null)
        {
            return _locationsByEvents;
        }

        lock (_eventsLock)
        {
            _locationsByEvents ??= scoringDbContext.RaceSeries.Select(oo => new Location(oo.State, oo.Area, oo.City)).ToList();
            return _locationsByEvents;
        }
    }
}
