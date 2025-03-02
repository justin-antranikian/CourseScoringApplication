using Api.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Api.Orchestration.Races.Search;

public class SearchRacesOrchestrator(ScoringDbContext dbContext)
{
    private const double MilesToMeters = 1609.34;

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
            const double distanceInMeters = 50 * MilesToMeters;
            query = query.Where(oo => oo.Location.IsWithinDistance(point, distanceInMeters));

            //var results1 = query.Select(oo => oo).ToListAsync();

        }

        var results = await query.OrderBy(oo => oo.OverallRank).Take(28).ToListAsync();
        return results.Select(MapToDto).ToList();
    }

    private static RaceSearchResultDto MapToDto(RaceSeries raceSeries)
    {
        var upcomingRace = raceSeries.Races.OrderByDescending(oo => oo.KickOffDate).First();
        var courses = upcomingRace.Courses.Select(oo => new DisplayNameWithIdDto(oo.Id, oo.Name)).ToList();

        return new RaceSearchResultDto
        {
            Id = raceSeries.Id,
            LocationInfoWithRank = raceSeries.ToLocationInfoWithRank(),
            Name = raceSeries.Name,
            RaceKickOffDate = upcomingRace.KickOffDate.ToShortDateString(),
            RaceSeriesType = raceSeries.RaceSeriesType.ToString(),
            UpcomingRaceId = upcomingRace.Id,
            Courses = courses,
        };
    }
}
