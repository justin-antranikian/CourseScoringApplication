using Api.DataModels;
using Api.Orchestration;
using Api.Orchestration.Races.GetLeaderboard;
using Api.Orchestration.Races.Search;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Geometries;
using NetTopologySuite;

namespace Api.Controllers;

[ApiController]
[Route("races")]
public class RacesController(ScoringDbContext dbContext) : ControllerBase
{
    [HttpGet("{raceId:int}")]
    public async Task<RaceLeaderboardDto> Get([FromRoute] int raceId)
    {
        var orchestrator = new GetRaceLeaderboardOrchestrator(dbContext);
        return await orchestrator.GetRaceLeaderboardDto(raceId);
    }

    [HttpGet("search")]
    public async Task<List<RaceSearchResultDto>> Get([FromQuery] SearchRacesRequest request)
    {
        var orchestrator = new SearchRacesOrchestrator(dbContext);
        return await orchestrator.Get(request);
    }

    [HttpGet("search2")]
    public async Task<List<RaceSearchResultDto>> Get()
    {
        const string coWkt = "POLYGON((-109.0448 37.0004,-102.0424 36.9949,-102.0534 41.0006,-109.0489 40.9996,-109.0448 37.0004,-109.0448 37.0004))";
        const string wyWkt = "POLYGON((-104.0556 41.0037,-104.0584 44.9949,-111.0539 44.9998,-111.0457 40.9986,-104.0556 41.0006,-104.0556 41.0037))";

        const string neWkt = "POLYGON((-104.0543 42.9986,-104.0543 41.0027,-102.0506 41.0006,-102.0493 40.0034,-95.3091 39.9992,-95.4808 40.2397,-95.6470 40.3130,-95.6689 40.4302,-95.7500 40.5900,-95.8543 40.6827,-95.8447 40.8138,-95.8324 40.9654,-95.8667 41.0794,-95.8722 41.2923,-95.9354 41.4458,-95.9999 41.5261,-96.0988 41.6380,-96.0686 41.7703,-96.1084 41.8368,-96.1372 41.9677,-96.2402 42.0330,-96.2746 42.1155,-96.3583 42.2021,-96.3281 42.2448,-96.4188 42.3890,-96.4037 42.4731,-96.6357 42.5369,-96.7099 42.6057,-96.6893 42.6532,-96.7621 42.6602,-96.8390 42.7147,-96.9763 42.7571,-97.2029 42.8085,-97.2290 42.8458,-97.3979 42.8629,-97.5133 42.8427,-97.6149 42.8488,-97.8456 42.8659,-97.9980 42.7470,-98.1450 42.8337,-98.4485 42.9293,-98.5020 42.9966,-104.0543 43.0006,-104.0543 42.9986))";
        const string ksWkt = "POLYGON((-102.0506 40.0034,-102.0506 40.0034,-102.0438 36.9927,-94.6211 36.9982,-94.6046 38.8803,-94.6143 39.0789,-94.6184 39.1971,-94.7255 39.1673,-94.8793 39.2759,-95.0990 39.5612,-94.8807 39.7283,-94.8930 39.8286,-94.9342 39.8823,-95.0098 39.8971,-95.0922 39.8760,-95.2213 39.9445,-95.3036 40.0087,-102.0506 40.0024,-102.0506 40.0034))";
        const string utWkt = "POLYGON((-114.0491 36.9982,-109.0462 37.0026,-109.0503 40.9986,-111.0471 41.0006,-111.0498 41.9993,-114.0395 41.9901,-114.0504 37.0015,-114.0491 36.9982))";

        var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory();
        var wktReader = new NetTopologySuite.IO.WKTReader(geometryFactory);
        var geometry = wktReader.Read(utWkt);

        var entity = new Athlete
        {
            AreaLocationId = 1,
            StateLocationId = 1,
            CityLocationId = 1,
            AreaRank = 0,
            CityRank = 0,
            DateOfBirth = new DateTime(1, 1, 1),
            FirstName = "First",
            FullName = "L",
            Location = geometry,
            Gender = Gender.Male,
            LastName = "L",
            OverallRank = 0,
            StateRank = 0
        };

        //var raceSeries = new RaceSeries
        //{
        //    AreaRank = 0,
        //    AreaLocationId = 1,
        //    StateLocationId = 1,
        //    CityLocationId = 1,
        //    CityRank = 0,
        //    Location = geometry,
        //    Name = "Baa",
        //    OverallRank = 0,
        //    RaceSeriesType = RaceSeriesType.Running,
        //    StateRank = 0
        //};
        
        dbContext.Athletes.AddRange(entity);
        await dbContext.SaveChangesAsync();
        return null;
    }
}