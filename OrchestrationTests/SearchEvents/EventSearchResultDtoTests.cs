using Core;
using DataModels;
using Orchestration.GetRaceSeriesSearch;
using System;
using System.Collections.Generic;
using Xunit;

namespace OrchestrationTests.SearchEvents;

public class EventSearchResultDtoTests
{
    [Fact]
    public void MapsAllFields()
    {
        var raceSeries = GetRaceSeries();
        var seriesDto = EventSearchResultDtoMapper.GetEventSearchResultDto(raceSeries);

        Assert.Equal(1, seriesDto.Id);
        Assert.Equal(RaceSeriesType.Triathalon, seriesDto.RaceSeriesType);
        Assert.Equal("Triathalon", seriesDto.RaceSeriesTypeName);
        Assert.Equal(2, seriesDto.UpcomingRaceId);
        Assert.Equal("1/1/2011", seriesDto.KickOffDate);
        Assert.Equal("06:30:00 AM", seriesDto.KickOffTime);
        Assert.Collection(seriesDto.Courses, course =>
        {
            Assert.Equal(1, course.Id);
            Assert.Equal("C1", course.DisplayName);
        }, course =>
        {
            Assert.Equal(2, course.Id);
            Assert.Equal("C2", course.DisplayName);
        });

        Assert.Equal(4, seriesDto.LocationInfoWithRank.OverallRank);
        Assert.Equal(8, seriesDto.Rating);
    }

    #region test preperation methods

    private static RaceSeries GetRaceSeries()
    {
        return new RaceSeries
        {
            Id = 1,
            RaceSeriesType = RaceSeriesType.Triathalon,
            Name = "AAA",
            Races = new()
            {
                new() { Id = 1, KickOffDate = new DateTime(2009, 1, 1) },
                new()
                {
                    Id = 2,
                    KickOffDate = new DateTime(2011, 1, 1, 6, 30, 0),
                    Courses = new()
                    {
                        new() { Id = 1, Name = "C1" },
                        new() { Id = 2, Name = "C2" },
                    }
                },
                new() { Id = 3, KickOffDate = new DateTime(2010, 1, 1) },
            },
            Description = "DDD",
            State = "SA",
            OverallRank = 4,
            Rating = 8
        };
    }

    #endregion
}
