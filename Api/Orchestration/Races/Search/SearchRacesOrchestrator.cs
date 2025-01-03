﻿using Api.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Api.Orchestration.Races.Search;

public class SearchRacesOrchestrator(ScoringDbContext dbContext)
{
    public async Task<List<RaceSearchResultDto>> Get(int? locationId, string? locationType, string? searchTerm)
    {
        var query = dbContext.GetRaceSeriesWithLocationInfo()
            .Include(oo => oo.Races)
            .ThenInclude(oo => oo.Courses)
            .AsQueryable();

        if (searchTerm != null)
        {
            query = query.Where(oo => oo.Name.Contains(searchTerm));
        }

        if (locationId.HasValue && locationType != null)
        {
            var type = Enum.Parse<LocationType>(locationType);
            var locationIdValue = locationId.Value;

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
