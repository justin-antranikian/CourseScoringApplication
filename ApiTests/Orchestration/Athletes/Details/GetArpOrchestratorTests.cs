using Api.DataModels;
using Api.Orchestration.Athletes.GetDetails;

namespace ApiTests.Orchestration.Athletes.Details;

public class GetArpOrchestratorTests
{
    [Fact]
    public async Task MapsAllFields()
    {
        var dbContext = ScoringDbContextCreator.GetScoringDbContext();

        var orchestrator = new GetAthleteDetailsOrchestrator(dbContext);
        var arpDto = await orchestrator.GetArpDto(1);

        Assert.Equal("FA LA", arpDto.FullName);
        Assert.Equal("FA", arpDto.FirstName);
        Assert.True(arpDto.Age >= 21);
        Assert.Equal("F", arpDto.GenderAbbreviated);
        Assert.Equal(new[] { "Triathlete", "Runner" }, arpDto.Tags);

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

        // results should be sorted from most recent to latest.
        Assert.Collection(arpDto.Results, result =>
        {
            Assert.Equal(3, result.AthleteCourseId);
            Assert.Equal(2, result.RaceId);
            Assert.Equal("2011 Houston Triathalon", result.RaceName);
            Assert.Equal(RaceSeriesType.Triathalon, result.RaceSeriesType);
            Assert.Equal(3, result.CourseId);
            Assert.Equal("Course 3", result.CourseName);
            Assert.Equal("Colorado", result.State);
            Assert.Equal("City 1", result.City);

            Assert.Equal(2, result.OverallRank);
            Assert.Equal(2, result.GenderRank);
            Assert.Equal(2, result.PrimaryDivisionRank);
            Assert.Equal(2, result.OverallCount);
            Assert.Equal(2, result.GenderCount);
            Assert.Equal(2, result.PrimaryDivisionCount);

            Assert.Equal("23:20", result.PaceWithTimeCumulative.TimeFormatted);
            Assert.True(result.PaceWithTimeCumulative.HasPace);
            Assert.Equal("mph", result.PaceWithTimeCumulative.PaceLabel);
            Assert.Equal("1.6", result.PaceWithTimeCumulative.PaceValue);
        }, result =>
        {
            Assert.Equal(1, result.AthleteCourseId);
            Assert.Equal(1, result.RaceId);
            Assert.Equal("2010 Houston Triathalon", result.RaceName);
            Assert.Equal(RaceSeriesType.Triathalon, result.RaceSeriesType);
            Assert.Equal(1, result.CourseId);
            Assert.Equal("Course 1", result.CourseName);
            Assert.Equal("Colorado", result.State);
            Assert.Equal("City 1", result.City);

            Assert.Equal(2, result.OverallRank);
            Assert.Equal(1, result.GenderRank);
            Assert.Equal(1, result.PrimaryDivisionRank);
            Assert.Equal(4, result.OverallCount);
            Assert.Equal(2, result.GenderCount);
            Assert.Equal(2, result.PrimaryDivisionCount);

            Assert.Equal("23:20", result.PaceWithTimeCumulative.TimeFormatted);
            Assert.False(result.PaceWithTimeCumulative.HasPace);
            Assert.Null(result.PaceWithTimeCumulative.PaceLabel);
            Assert.Null(result.PaceWithTimeCumulative.PaceValue);
        }, result =>
        {
            Assert.Equal(2, result.AthleteCourseId);
            Assert.Equal(1, result.RaceId);
            Assert.Equal("2010 Houston Triathalon", result.RaceName);
            Assert.Equal(RaceSeriesType.Triathalon, result.RaceSeriesType);
            Assert.Equal(2, result.CourseId);
            Assert.Equal("Course 2", result.CourseName);
            Assert.Equal("Colorado", result.State);
            Assert.Equal("City 1", result.City);

            Assert.Equal(1, result.OverallRank);
            Assert.Equal(1, result.GenderRank);
            Assert.Equal(1, result.PrimaryDivisionRank);
            Assert.Equal(4, result.OverallCount);
            Assert.Equal(4, result.GenderCount);
            Assert.Equal(4, result.PrimaryDivisionCount);

            Assert.Equal("11:40", result.PaceWithTimeCumulative.TimeFormatted);
            Assert.True(result.PaceWithTimeCumulative.HasPace);
            Assert.Equal("mph", result.PaceWithTimeCumulative.PaceLabel);
            Assert.Equal("3.2", result.PaceWithTimeCumulative.PaceValue);
        });

        Assert.Collection(arpDto.Goals, result =>
        {
            Assert.Equal("All Events", result.RaceSeriesTypeName);
            Assert.Equal(16, result.GoalTotal);
            Assert.Equal(3, result.ActualTotal);
            Assert.Equal(8000, result.TotalDistance);
            Assert.Equal(19, result.PercentComplete);
            Assert.Equal(3, result.Courses.Count);
        }, result =>
        {
            Assert.Equal("Running", result.RaceSeriesTypeName);
            Assert.Equal(1, result.GoalTotal);
            Assert.Equal(0, result.ActualTotal);
            Assert.Equal(0, result.TotalDistance);
            Assert.Equal(0, result.PercentComplete);
            Assert.Empty(result.Courses);
        }, result =>
        {
            Assert.Equal("Triathalon", result.RaceSeriesTypeName);
            Assert.Equal(15, result.GoalTotal);
            Assert.Equal(3, result.ActualTotal);
            Assert.Equal(8000, result.TotalDistance);
            Assert.Equal(20, result.PercentComplete);
            Assert.Equal(3, result.Courses.Count);
        }, result =>
        {
            Assert.Equal("Road Biking", result.RaceSeriesTypeName);
            Assert.Empty(result.Courses);
        }, result =>
        {
            Assert.Equal("Mountain Biking", result.RaceSeriesTypeName);
            Assert.Empty(result.Courses);
        }, result =>
        {
            Assert.Equal("Cross Country Skiing", result.RaceSeriesTypeName);
            Assert.Empty(result.Courses);
        }, result =>
        {
            Assert.Equal("Swimming", result.RaceSeriesTypeName);
            Assert.Empty(result.Courses);
        });

        Assert.Equal("All Events", arpDto.AllEventsGoal.RaceSeriesTypeName);
        Assert.Equal(16, arpDto.AllEventsGoal.GoalTotal);
        Assert.Equal(19, arpDto.AllEventsGoal.PercentComplete);

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
        foreach(var entry in wellnessEntries)
        {
            Assert.Equal(type, entry.AthleteWellnessType);
            Assert.Equal(descriptions[count], entry.Description);
            count++;
        }
    }
}
