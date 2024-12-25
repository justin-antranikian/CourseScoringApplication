﻿//using Api.DataModels;
//using Microsoft.EntityFrameworkCore;

//namespace Api.Orchestration.Athletes.Search;

//public class SearchAthletesOrchestrator(ScoringDbContext scoringDbContext)
//{
//    public async Task<List<AthleteSearchResultDto>> GetSearchResults(SearchAthletesRequestDto searchRequestDto)
//    {
//        var baseQuery = scoringDbContext.Athletes.Include(oo => oo.AthleteRaceSeriesGoals).AsQueryable();

//        if (searchRequestDto.SearchTerm is string searchTerm)
//        {
//            baseQuery = baseQuery.Where(oo => oo.FullName.StartsWith(searchTerm));
//        }

//        if (searchRequestDto.State is string stateUrl)
//        {
//            var location = LocationHelper.Find(oo => oo.StateUrlFriendly == stateUrl);
//            baseQuery = baseQuery.Where(oo => oo.State == location.State);
//        }

//        if (searchRequestDto.Area is string areaUrl)
//        {
//            var location = LocationHelper.Find(oo => oo.AreaUrlFriendly == areaUrl);
//            baseQuery = baseQuery.Where(oo => oo.Area == location.Area);
//        }

//        if (searchRequestDto.City is string cityUrl)
//        {
//            var location = LocationHelper.Find(oo => oo.CityUrlFriendly == cityUrl);
//            baseQuery = baseQuery.Where(oo => oo.City == location.City);
//        }

//        var athletes = await baseQuery.OrderBy(oo => oo.OverallRank).Take(28).ToListAsync();
//        return athletes.Select(oo => AthleteSearchResultDtoMapper.GetAthleteSearchResultDto(oo)).ToList();
//    }
//}
