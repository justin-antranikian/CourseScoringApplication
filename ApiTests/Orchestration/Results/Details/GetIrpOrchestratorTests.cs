using Api.Orchestration.Results.GetDetails;

namespace ApiTests.Orchestration.Results.Details;

public class GetIrpOrchestratorTests
{
    [Fact]
    public async Task MapsAllFields()
    {
        var dbContext = ScoringDbContextCreator.GetScoringDbContext();
        var orchestrator = new GetIrpOrchestrator(dbContext);
        var irpDto = await orchestrator.Get(1);

        Assert.Equal(1, irpDto.AthleteId);
        Assert.Equal("FA LA", irpDto.FullName);
        Assert.Equal(10, irpDto.RaceAge);
        Assert.Equal("F", irpDto.GenderAbbreviated);
        Assert.Equal("BA", irpDto.Bib);
        Assert.Equal("23:20", irpDto.PaceWithTimeCumulative.TimeFormatted);
        Assert.False(irpDto.PaceWithTimeCumulative.HasPace);
        Assert.Null(irpDto.PaceWithTimeCumulative.PaceValue);
        Assert.Null(irpDto.PaceWithTimeCumulative.PaceLabel);

        Assert.Equal("FA", irpDto.FirstName);
        Assert.Equal("PST", irpDto.TimeZoneAbbreviated);
        Assert.Null(irpDto.FinishTime);
        Assert.Equal(new[] { "Triathlete", "Runner" }, irpDto.Tags);
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

        Assert.Collection(irpDto.BracketResults, result =>
        {
            Assert.Equal("Overall", result.Name);
            Assert.Equal(2, result.Rank);
            Assert.Equal(4, result.TotalRacers);
        }, result =>
        {
            Assert.Equal("Female", result.Name);
            Assert.Equal(1, result.Rank);
            Assert.Equal(2, result.TotalRacers);
        }, result =>
        {
            Assert.Equal("M20-30", result.Name);
            Assert.Equal(1, result.Rank);
            Assert.Equal(2, result.TotalRacers);
        }, result =>
        {
            Assert.Equal("Florida 20-30", result.Name);
            Assert.Equal(1, result.Rank);
            Assert.Equal(2, result.TotalRacers);
        });

        Assert.Collection(irpDto.IntervalResults, result =>
        {
            Assert.Equal("Swim", result.IntervalName);
            Assert.Equal("11:40", result.PaceWithTimeCumulative.TimeFormatted);
            Assert.False(result.PaceWithTimeCumulative.HasPace);
            Assert.Null(result.PaceWithTimeCumulative.PaceValue);
            Assert.Null(result.PaceWithTimeCumulative.PaceLabel);
            Assert.Equal("11:40", result.PaceWithTimeIntervalOnly.TimeFormatted);
            Assert.True(result.PaceWithTimeIntervalOnly.HasPace);
            Assert.Equal("1:10", result.PaceWithTimeIntervalOnly.PaceValue);
            Assert.Equal("min/100m", result.PaceWithTimeIntervalOnly.PaceLabel);
            Assert.Equal(1, result.OverallRank);
            Assert.Equal(1, result.GenderRank);
            Assert.Equal(1, result.PrimaryDivisionRank);
            Assert.Equal(4, result.OverallCount);
            Assert.Equal(2, result.GenderCount);
            Assert.Equal(2, result.PrimaryDivisionCount);
            Assert.Equal(BetweenIntervalTimeIndicator.StartingOrSame, result.OverallIndicator);
            Assert.Equal(BetweenIntervalTimeIndicator.StartingOrSame, result.GenderIndicator);
            Assert.Equal(BetweenIntervalTimeIndicator.StartingOrSame, result.PrimaryDivisionIndicator);
            Assert.Equal("12:11:40 AM", result.CrossingTime);
            Assert.False(result.IsFullCourse);
        }, result =>
        {
            Assert.Equal("Transition 1", result.IntervalName);
            Assert.Equal("23:20", result.PaceWithTimeCumulative.TimeFormatted);
            Assert.False(result.PaceWithTimeCumulative.HasPace);
            Assert.Null(result.PaceWithTimeCumulative.PaceValue);
            Assert.Null(result.PaceWithTimeCumulative.PaceLabel);
            Assert.Equal("11:40", result.PaceWithTimeIntervalOnly.TimeFormatted);
            Assert.False(result.PaceWithTimeIntervalOnly.HasPace);
            Assert.Null(result.PaceWithTimeIntervalOnly.PaceValue);
            Assert.Null(result.PaceWithTimeIntervalOnly.PaceLabel);
            Assert.Equal(1, result.OverallRank);
            Assert.Equal(1, result.GenderRank);
            Assert.Equal(1, result.PrimaryDivisionRank);
            Assert.Equal(4, result.OverallCount);
            Assert.Equal(2, result.GenderCount);
            Assert.Equal(2, result.PrimaryDivisionCount);
            Assert.Equal(BetweenIntervalTimeIndicator.StartingOrSame, result.OverallIndicator);
            Assert.Equal(BetweenIntervalTimeIndicator.StartingOrSame, result.GenderIndicator);
            Assert.Equal(BetweenIntervalTimeIndicator.StartingOrSame, result.PrimaryDivisionIndicator);
            Assert.Equal("12:23:20 AM", result.CrossingTime);
            Assert.False(result.IsFullCourse);
        }, result =>
        {
            Assert.Equal("Bike", result.IntervalName);
            Assert.Null(result.PaceWithTimeCumulative);
            Assert.Null(result.PaceWithTimeIntervalOnly);
            Assert.Null(result.OverallRank);
            Assert.Null(result.GenderRank);
            Assert.Null(result.PrimaryDivisionRank);
            Assert.Equal(4, result.OverallCount);
            Assert.Equal(2, result.GenderCount);
            Assert.Equal(2, result.PrimaryDivisionCount);
            Assert.Equal(BetweenIntervalTimeIndicator.NotStarted, result.OverallIndicator);
            Assert.Equal(BetweenIntervalTimeIndicator.NotStarted, result.GenderIndicator);
            Assert.Equal(BetweenIntervalTimeIndicator.NotStarted, result.PrimaryDivisionIndicator);
            Assert.Null(result.CrossingTime);
            Assert.False(result.IsFullCourse);
        }, result =>
        {
            Assert.Equal("Transition 2", result.IntervalName);
            Assert.Null(result.PaceWithTimeCumulative);
            Assert.Null(result.PaceWithTimeIntervalOnly);
            Assert.Null(result.OverallRank);
            Assert.Null(result.GenderRank);
            Assert.Null(result.PrimaryDivisionRank);
            Assert.Equal(4, result.OverallCount);
            Assert.Equal(2, result.GenderCount);
            Assert.Equal(2, result.PrimaryDivisionCount);
            Assert.Equal(BetweenIntervalTimeIndicator.NotStarted, result.OverallIndicator);
            Assert.Equal(BetweenIntervalTimeIndicator.NotStarted, result.GenderIndicator);
            Assert.Equal(BetweenIntervalTimeIndicator.NotStarted, result.PrimaryDivisionIndicator);
            Assert.Null(result.CrossingTime);
            Assert.False(result.IsFullCourse);
        }, result =>
        {
            Assert.Equal("Run", result.IntervalName);
            Assert.Null(result.PaceWithTimeCumulative);
            Assert.Null(result.PaceWithTimeIntervalOnly);
            Assert.Null(result.OverallRank);
            Assert.Null(result.GenderRank);
            Assert.Null(result.PrimaryDivisionRank);
            Assert.Equal(4, result.OverallCount);
            Assert.Equal(2, result.GenderCount);
            Assert.Equal(2, result.PrimaryDivisionCount);
            Assert.Equal(BetweenIntervalTimeIndicator.NotStarted, result.OverallIndicator);
            Assert.Equal(BetweenIntervalTimeIndicator.NotStarted, result.GenderIndicator);
            Assert.Equal(BetweenIntervalTimeIndicator.NotStarted, result.PrimaryDivisionIndicator);
            Assert.Null(result.CrossingTime);
            Assert.False(result.IsFullCourse);
        }, result =>
        {
            Assert.Equal("Full Course", result.IntervalName);
            Assert.Null(result.PaceWithTimeCumulative);
            Assert.Null(result.PaceWithTimeIntervalOnly);
            Assert.Null(result.OverallRank);
            Assert.Null(result.GenderRank);
            Assert.Null(result.PrimaryDivisionRank);
            Assert.Equal(4, result.OverallCount);
            Assert.Equal(2, result.GenderCount);
            Assert.Equal(2, result.PrimaryDivisionCount);
            Assert.Equal(BetweenIntervalTimeIndicator.NotStarted, result.OverallIndicator);
            Assert.Equal(BetweenIntervalTimeIndicator.NotStarted, result.GenderIndicator);
            Assert.Equal(BetweenIntervalTimeIndicator.NotStarted, result.PrimaryDivisionIndicator);
            Assert.Null(result.CrossingTime);
            Assert.True(result.IsFullCourse);
        });
    }

    [Fact]
    public async Task WithCumulativePace_MapsAllFields()
    {
        var dbContext = ScoringDbContextCreator.GetScoringDbContext();

        var orchestrator = new GetIrpOrchestrator(dbContext);
        var irpDto = await orchestrator.Get(6);

        Assert.Equal("46:44", irpDto.PaceWithTimeCumulative.TimeFormatted);
        Assert.True(irpDto.PaceWithTimeCumulative.HasPace);
        Assert.Equal("1.6", irpDto.PaceWithTimeCumulative.PaceValue);
        Assert.Equal("mph", irpDto.PaceWithTimeCumulative.PaceLabel);
    }
}
