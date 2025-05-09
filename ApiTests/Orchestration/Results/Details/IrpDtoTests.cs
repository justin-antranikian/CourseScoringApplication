﻿using Api.DataModels;
using Api.Orchestration;
using Api.Orchestration.Results.GetDetails;
using NetTopologySuite.Geometries;
using Location = Api.DataModels.Location;

namespace ApiTests.Orchestration.Results.Details;

public class IrpDtoTests
{
    [Fact]
    public void MapsAllFields()
    {
        var irpDto = GetIrpDto();

        Assert.Equal(1, irpDto.AthleteId);
        Assert.Equal("FA LA", irpDto.FullName);
        Assert.Equal(10, irpDto.RaceAge);
        Assert.Equal("M", irpDto.GenderAbbreviated);
        Assert.Equal("BA", irpDto.Bib);

        Assert.Equal("01:01", irpDto.PaceWithTimeCumulative.TimeFormatted);
        Assert.False(irpDto.PaceWithTimeCumulative.HasPace);
        Assert.Null(irpDto.PaceWithTimeCumulative.PaceValue);
        Assert.Null(irpDto.PaceWithTimeCumulative.PaceLabel);

        Assert.Equal("FA", irpDto.FirstName);
        Assert.Equal("PST", irpDto.TimeZoneAbbreviated);
        Assert.Null(irpDto.FinishTime);
        Assert.Empty(irpDto.Tags);
        Assert.Empty(irpDto.TrainingList);
        Assert.Equal("", irpDto.CourseGoalDescription);
        Assert.Equal("", irpDto.PersonalGoalDescription);

        Assert.Equal("CA", irpDto.LocationInfoWithRank.City);
        Assert.Equal("AA", irpDto.LocationInfoWithRank.Area);
        Assert.Equal("SA", irpDto.LocationInfoWithRank.State);
        Assert.Equal("ca", irpDto.LocationInfoWithRank.CityUrl);
        Assert.Equal("aa", irpDto.LocationInfoWithRank.AreaUrl);
        Assert.Equal("sa", irpDto.LocationInfoWithRank.StateUrl);
        Assert.Equal(1, irpDto.LocationInfoWithRank.CityRank);
        Assert.Equal(2, irpDto.LocationInfoWithRank.AreaRank);
        Assert.Equal(3, irpDto.LocationInfoWithRank.StateRank);
        Assert.Equal(4, irpDto.LocationInfoWithRank.OverallRank);

        Assert.Empty(irpDto.BracketResults);

        Assert.Collection(irpDto.IntervalResults, result =>
        {
            Assert.Equal("full course", result.IntervalName);
            Assert.True(result.IsFullCourse);
        });
    }

    #region test preperation methods

    private static IrpDto GetIrpDto()
    {
        var athlete = new Athlete
        {
            Id = 1,
            FirstName = "FA",
            LastName = "LA",
            FullName = "FA LA",
            Gender = Gender.Male,
            OverallRank = 4,
            StateRank = 3,
            AreaRank = 2,
            CityRank = 1,
            CityLocation = new Location
            {
                LocationType = LocationType.City,
                Name = "CA",
                Slug = "ca"
            },
            AreaLocation = new Location
            {
                LocationType = LocationType.Area,
                Name = "AA",
                Slug = "aa"
            },
            StateLocation = new Location
            {
                LocationType = LocationType.State,
                Name = "SA",
                Slug = "sa"
            },
            DateOfBirth = new DateTime(2000, 1, 1),
        };

        var athleteCourse = new AthleteCourse
        {
            AthleteId = 1,
            Bib = "BA",
            Athlete = athlete,
            CourseGoalDescription = "",
            PersonalGoalDescription = ""
        };

        var course = new Course
        {
            Id = 1,
            Name = "CA",
            StartDate = new DateTime(2010, 1, 1),
            Distance = 1000,
            RaceId = 1,
            Race = new()
            {
                Name = "RA",
                RaceSeries = new()
                {
                    RaceSeriesType = RaceSeriesType.Triathalon,
                    AreaRank = 0,
                    AreaLocation = new Location
                    {
                        LocationType = LocationType.Area,
                        Name = "A",
                        Slug = "a"
                    },
                    CityRank = 0,
                    Location = new Point(100, 100),
                    Name = "",
                    OverallRank = 0,
                    StateRank = 0
                },
                TimeZoneId = "Pacific Standard Time",
                KickOffDate = default
            },
            CourseType = CourseType.Running5K,
            PaceType = PaceType.None,
            PreferedMetric = PreferredMetric.Metric,
            SortOrder = 0
        };

        var paceWithTime = new PaceWithTime("01:01", false);

        var intervalResults = new IrpResultByIntervalDto[]
        {
            new()
            {
                IntervalName = "full course",
                PaceWithTimeCumulative = null,
                PaceWithTimeIntervalOnly = null,
                OverallRank = null,
                GenderRank = null,
                PrimaryDivisionRank = null,
                OverallCount = 0,
                GenderCount = 0,
                PrimaryDivisionCount = 0,
                OverallIndicator = BetweenIntervalTimeIndicator.NotStarted,
                GenderIndicator = BetweenIntervalTimeIndicator.NotStarted,
                PrimaryDivisionIndicator = BetweenIntervalTimeIndicator.NotStarted,
                CrossingTime = null,
                IsFullCourse = true
            }
        };

        return IrpDtoMapper.GetIrpDto(athleteCourse.Athlete, athleteCourse, course, paceWithTime, [], intervalResults.ToList());
    }

    #endregion
}
