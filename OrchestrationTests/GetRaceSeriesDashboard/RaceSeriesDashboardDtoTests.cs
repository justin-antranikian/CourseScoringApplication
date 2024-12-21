using Core;
using Core.Enums;
using System;
using System.Collections.Generic;
using Api.DataModels;
using Api.Orchestration.GetRaceSeriesDashboard;
using Xunit;

namespace OrchestrationTests.GetRaceSeriesDashboard;

public class RaceSeriesDashboardDtoTests
{
    [Fact]
    public void MapsAllFields()
    {
        var raceSeries = new RaceSeries
        {
            Name = "Race Series 1",
            Description = "Description 1",
            RaceSeriesType = RaceSeriesType.RoadBiking,
            State = "Colorado",
            Area = "Greater Den",
            City = "Den",
            OverallRank = 4,
            StateRank = 3,
            AreaRank = 2,
            CityRank = 1
        };

        var races = new List<PastRaceDto>
        {
            new (1, "Race 1", new DateTime(2010, 1, 1).ToShortDateString())
        };

        var courseInformationEntries = new List<CourseInformationEntry>
        {
            new (CourseInformationType.Description, "")
        };

        var courses = new List<RaceSeriesDashboardCourseDto>
        {
            new (1, "Course 1", courseInformationEntries, new())
        };

        var raceSeriesDto = RaceSeriesDashboardDtoMapper.GetRaceSeriesDashboardDto(raceSeries, races, courses);

        Assert.Equal("Race Series 1", raceSeriesDto.Name);
        Assert.Equal("Description 1", raceSeriesDto.Description);
        Assert.Equal("1/1/2010", raceSeriesDto.KickOffDate);
        Assert.Single(raceSeriesDto.Races);
        Assert.Single(raceSeriesDto.Courses);
        Assert.Equal(RaceSeriesType.RoadBiking, raceSeriesDto.RaceSeriesType);

        var locationInfo = raceSeriesDto.LocationInfoWithRank;
        Assert.Equal(4, locationInfo.OverallRank);
        Assert.Equal("Colorado", locationInfo.State);
        Assert.Equal("colorado", locationInfo.StateUrl);
        Assert.Equal(3, locationInfo.StateRank);
        Assert.Equal("Greater Den", locationInfo.Area);
        Assert.Equal("greater-den", locationInfo.AreaUrl);
        Assert.Equal(2, locationInfo.AreaRank);
        Assert.Equal("Den", locationInfo.City);
        Assert.Equal("den", locationInfo.CityUrl);
        Assert.Equal(1, locationInfo.CityRank);

        Assert.Equal(1, raceSeriesDto.UpcomingRaceId);
        Assert.Equal(1, raceSeriesDto.FirstCourseId);
    }
}
