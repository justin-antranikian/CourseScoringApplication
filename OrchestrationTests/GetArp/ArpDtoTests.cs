using Core;
using Core.Enums;
using DataModels;
using Orchestration.GetArp;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace OrchestrationTests.GetArp;

public class ArpDtoTests
{
    [Fact]
    public void MapsAllFields()
    {
        var athlete = GetAthlete();
        var (results, goals) = GetResultsAndGoals();
        var alleventsGoal = new ArpGoalDto("All Events", 16, 3, 5000, 50, new List<CourseGoalArpDto>());

        var arpDto = ArpDtoMapper.GetArpDto(athlete, results, goals, alleventsGoal);

        Assert.Equal("FA", arpDto.FirstName);
        Assert.Equal("FA LA", arpDto.FullName);
        Assert.True(arpDto.Age >= 21);
        Assert.Equal("M", arpDto.GenderAbbreviated);
        Assert.Equal(new[] { "Triathlete", "Swimmer" }, arpDto.Tags);

        var location = arpDto.LocationInfoWithRank;
        Assert.Equal(4, location.OverallRank);
        Assert.Equal("SA", location.State);
        Assert.Equal("sa", location.StateUrl);
        Assert.Equal(3, location.StateRank);
        Assert.Equal("AA", location.Area);
        Assert.Equal("aa", location.AreaUrl);
        Assert.Equal(2, location.AreaRank);
        Assert.Equal("CA", location.City);
        Assert.Equal("ca", location.CityUrl);
        Assert.Equal(1, location.CityRank);

        Assert.Collection(arpDto.Results, result =>
        {
            Assert.Equal(40, result.OverallCount);
        });

        Assert.Collection(arpDto.Goals, result =>
        {
            Assert.Equal("All Events", result.RaceSeriesTypeName);
            Assert.Equal(16, result.GoalTotal);
            Assert.Equal(50, result.PercentComplete);
        }, result =>
        {
            Assert.Equal("Triathalon", result.RaceSeriesTypeName);
            Assert.Equal(15, result.GoalTotal);
            Assert.Equal(20, result.PercentComplete);
        }, result =>
        {
            Assert.Equal("Swimming", result.RaceSeriesTypeName);
            Assert.Equal(1, result.GoalTotal);
            Assert.Equal(10, result.PercentComplete);
        });

        Assert.Equal("All Events", arpDto.AllEventsGoal.RaceSeriesTypeName);
        Assert.Equal(16, arpDto.AllEventsGoal.GoalTotal);
        Assert.Equal(50, arpDto.AllEventsGoal.PercentComplete);

        Assert.Collection(arpDto.WellnessTrainingAndDiet, result =>
        {
            Assert.Equal(AthleteWellnessType.Diet, result.AthleteWellnessType);
            Assert.Equal("D1", result.Description);
        }, result =>
        {
            Assert.Equal(AthleteWellnessType.Training, result.AthleteWellnessType);
            Assert.Equal("T1", result.Description);
        }, result =>
        {
            Assert.Equal(AthleteWellnessType.Training, result.AthleteWellnessType);
            Assert.Equal("T2", result.Description);
        }, result =>
        {
            Assert.Equal(AthleteWellnessType.Training, result.AthleteWellnessType);
            Assert.Equal("T3", result.Description);
        });

        AssertWellnessEntries(arpDto.WellnessGoals, AthleteWellnessType.Goal, "G1");
        AssertWellnessEntries(arpDto.WellnessGoals, AthleteWellnessType.Goal, "G1");
        AssertWellnessEntries(arpDto.WellnessGearList, AthleteWellnessType.Gear, "G1", "G2");
        AssertWellnessEntries(arpDto.WellnessMotivationalList, AthleteWellnessType.Motivational, "M1", "M2");
    }

    private static void AssertWellnessEntries(List<AthleteWellnessEntryDto> wellnessEntries, AthleteWellnessType type, params string[] descriptions)
    {
        var count = 0;
        foreach (var entry in wellnessEntries)
        {
            Assert.Equal(type, entry.AthleteWellnessType);
            Assert.Equal(descriptions[count], entry.Description);
            count++;
        }
    }

    #region test preperation methods

    private static Athlete GetAthlete()
    {
        return new Athlete
        {
            Id = 1,
            FullName = "FA LA",
            FirstName = "FA",
            State = "SA",
            Area = "AA",
            City = "CA",
            OverallRank = 4,
            StateRank = 3,
            AreaRank = 2,
            CityRank = 1,
            Gender = Gender.Male,
            AthleteRaceSeriesGoals = new()
            {
                new (RaceSeriesType.Triathalon, 15),
                new (RaceSeriesType.Swim, 1),
            },
            AthleteWellnessEntries = new()
            {
                new (AthleteWellnessType.Gear, "G1"),
                new (AthleteWellnessType.Gear, "G2"),
                new (AthleteWellnessType.Diet, "D1"),
                new (AthleteWellnessType.Goal, "G1"),
                new (AthleteWellnessType.Motivational, "M1"),
                new (AthleteWellnessType.Motivational, "M2"),
                new (AthleteWellnessType.Training, "T1"),
                new (AthleteWellnessType.Training, "T2"),
                new (AthleteWellnessType.Training, "T3"),
            }
        };
    }

    private static (List<ArpResultDto>, List<ArpGoalDto>) GetResultsAndGoals()
    {
        var results = new List<ArpResultDto>
        {
            new (1, 1, "RA", RaceSeriesType.CrossCountrySkiing, 1, "CA", "SA", "CA", 4, 3, 2, 40, 30, 20, null),
        };

        var goals = new List<ArpGoalDto>
        {
            new ("Triathalon", 15, 2, 5000, 20, null),
            new ("Swimming", 1, 1, 1000, 10, null),
        };

        return (results, goals);
    }

    private static List<Athlete> GetAthletes()
    {
        return new()
        {
            new Athlete { Id = 2, FullName = "FA LA 2" },
            new Athlete { Id = 3, FullName = "FA LA 3" },
            new Athlete { Id = 4, FullName = "FA LA 4" },
        };
    }

    #endregion
}
