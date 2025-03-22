using Api.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Api.Orchestration.Races.Search;

public class SearchRacesOrchestrator(ScoringDbContext dbContext)
{
    public async Task<List<RaceSearchResultDto>> Get(SearchRacesRequest request)
    {
        var query = dbContext.GetRaceSeriesWithLocationInfo()
            .Include(oo => oo.Races)
            .ThenInclude(oo => oo.Courses)
            .AsQueryable();

        if (request.SearchTerm != null)
        {
            query = query.Where(oo => oo.Name.Contains(request.SearchTerm));
        }

        if (request.LocationId.HasValue && request.LocationType != null)
        {
            var type = Enum.Parse<LocationType>(request.LocationType);
            var locationIdValue = request.LocationId.Value;

            IQueryable<RaceSeries> GetQuery()
            {
                return type switch
                {
                    LocationType.State => query.Where(oo => oo.StateLocationId == locationIdValue),
                    LocationType.Area => query.Where(oo => oo.AreaLocationId == locationIdValue),
                    LocationType.City => query.Where(oo => oo.CityLocationId == locationIdValue),
                };
            }

            query = GetQuery();
        }

        if (request.Longitude.HasValue)
        {
            var point = GeometryExtensions.CreatePoint(request.Latitude!.Value, request.Longitude.Value);
            const double distanceInMeters = 50 * GeometryExtensions.MilesToMeters;
            query = query.Where(oo => oo.Location.IsWithinDistance(point, distanceInMeters)).OrderBy(oo => oo.Location.Distance(point));
            var resultsSet = await query.Select(oo => new { distance = oo.Location.Distance(point) / GeometryExtensions.MilesToMeters, raceSeries = oo }).ToListAsync();
            return resultsSet.Select(oo => MapToDto(oo.raceSeries, oo.distance)).ToList();
        }
        else
        {
            query = query.OrderBy(oo => oo.OverallRank);
        }

        var results = await query.Take(28).ToListAsync();
        return results.Select(oo => MapToDto(oo)).ToList();
    }

    private static RaceSearchResultDto MapToDto(RaceSeries raceSeries, double? distance = null)
    {
        var upcomingRace = raceSeries.Races.OrderByDescending(oo => oo.KickOffDate).First();
        var courses = upcomingRace.Courses.Select(oo => new DisplayNameWithIdDto(oo.Id, oo.Name)).ToList();

        return new RaceSearchResultDto
        {
            Id = raceSeries.Id,
            Distance = distance,
            LocationInfoWithRank = raceSeries.ToLocationInfoWithRank(),
            Name = raceSeries.Name,
            RaceKickOffDate = upcomingRace.KickOffDate.ToShortDateString(),
            RaceSeriesType = raceSeries.RaceSeriesType.ToString(),
            UpcomingRaceId = upcomingRace.Id,
            Courses = courses,
        };
    }
}
