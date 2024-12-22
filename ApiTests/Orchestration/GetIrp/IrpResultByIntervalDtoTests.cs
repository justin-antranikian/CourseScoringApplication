using Api.DataModels;
using Api.Orchestration;
using Api.Orchestration.Results.GetDetails;

namespace ApiTests.Orchestration.GetIrp;

public class IrpResultByIntervalDtoTests
{
    [Fact]
    public void IncompleteInterval_IsMappedCorrectly()
    {
        var course = GetCourse();
        var interval = GetInterval(IntervalType.Transition);
        var metadataTotalHelper = GetMetadataGetTotalHelper();

        var intervalDto = IrpResultByIntervalDtoMapper.GetIrpResultByIntervalDto(course, interval, null, null, metadataTotalHelper);

        Assert.NotNull(intervalDto);
        Assert.Equal("NA", intervalDto.IntervalName);

        Assert.Null(intervalDto.PaceWithTimeCumulative);
        Assert.Null(intervalDto.PaceWithTimeIntervalOnly);

        Assert.Null(intervalDto.OverallRank);
        Assert.Null(intervalDto.GenderRank);
        Assert.Null(intervalDto.PrimaryDivisionRank);
        Assert.Equal(30, intervalDto.OverallCount);
        Assert.Equal(20, intervalDto.GenderCount);
        Assert.Equal(10, intervalDto.PrimaryDivisionCount);
        Assert.Equal(BetweenIntervalTimeIndicator.NotStarted, intervalDto.OverallIndicator);
        Assert.Equal(BetweenIntervalTimeIndicator.NotStarted, intervalDto.GenderIndicator);
        Assert.Equal(BetweenIntervalTimeIndicator.NotStarted, intervalDto.PrimaryDivisionIndicator);
        Assert.Null(intervalDto.CrossingTime);
        Assert.False(intervalDto.IsFullCourse);
    }

    [Fact]
    public void FirstCompletedInterval_IsMappedCorrectly()
    {
        var course = GetCourse();
        var interval = GetInterval(IntervalType.Transition);
        var metadataTotalHelper = GetMetadataGetTotalHelper();
        var result = GetResult(1001);

        var intervalDto = IrpResultByIntervalDtoMapper.GetIrpResultByIntervalDto(course, interval, result, null, metadataTotalHelper);

        Assert.NotNull(intervalDto);
        Assert.Equal("NA", intervalDto.IntervalName);

        var cumulativePace = intervalDto.PaceWithTimeCumulative;
        Assert.Equal("16:41", cumulativePace.TimeFormatted);
        Assert.True(cumulativePace.HasPace);
        Assert.Equal("min/mi", cumulativePace.PaceLabel);
        Assert.Equal("1:20", cumulativePace.PaceValue);

        var intervalPace = intervalDto.PaceWithTimeIntervalOnly;
        Assert.Equal("16:41", intervalPace.TimeFormatted);
        Assert.False(intervalPace.HasPace);
        Assert.Null(intervalPace.PaceLabel);
        Assert.Null(intervalPace.PaceValue);

        Assert.Equal(4, intervalDto.OverallRank);
        Assert.Equal(3, intervalDto.GenderRank);
        Assert.Equal(2, intervalDto.PrimaryDivisionRank);
        Assert.Equal(30, intervalDto.OverallCount);
        Assert.Equal(20, intervalDto.GenderCount);
        Assert.Equal(10, intervalDto.PrimaryDivisionCount);
        Assert.Equal(BetweenIntervalTimeIndicator.StartingOrSame, intervalDto.OverallIndicator);
        Assert.Equal(BetweenIntervalTimeIndicator.StartingOrSame, intervalDto.GenderIndicator);
        Assert.Equal(BetweenIntervalTimeIndicator.StartingOrSame, intervalDto.PrimaryDivisionIndicator);
        Assert.Equal("12:16:41 AM", intervalDto.CrossingTime);
        Assert.False(intervalDto.IsFullCourse);
    }

    [Fact]
    public void FullCourseInterval_IsMappedCorrectly()
    {
        var course = GetCourse();
        var interval = GetInterval(IntervalType.FullCourse, 20000, PaceType.MinuteMileOrKilometer);
        var metadataTotalHelper = GetMetadataGetTotalHelper();
        var result = GetResult(1001);

        var previousResult = new IrpResultByIntervalDto
        {
            IntervalName = "null",
            PaceWithTimeCumulative = null,
            PaceWithTimeIntervalOnly = null,
            OverallRank = 10,
            GenderRank = 10,
            PrimaryDivisionRank = 10,
            OverallCount = 0,
            GenderCount = 0,
            PrimaryDivisionCount = 0,
            OverallIndicator = BetweenIntervalTimeIndicator.GettingWorse,
            GenderIndicator = BetweenIntervalTimeIndicator.GettingWorse,
            PrimaryDivisionIndicator = BetweenIntervalTimeIndicator.GettingWorse,
            CrossingTime = "",
            IsFullCourse = false
        };

        var intervalDto = IrpResultByIntervalDtoMapper.GetIrpResultByIntervalDto(course, interval, result, previousResult, metadataTotalHelper);

        Assert.NotNull(intervalDto);
        Assert.Equal("NA", intervalDto.IntervalName);

        var cumulativePace = intervalDto.PaceWithTimeCumulative;
        Assert.Equal("16:41", cumulativePace.TimeFormatted);
        Assert.True(cumulativePace.HasPace);
        Assert.Equal("min/mi", cumulativePace.PaceLabel);
        Assert.Equal("1:20", cumulativePace.PaceValue);

        var intervalPace = intervalDto.PaceWithTimeIntervalOnly;
        Assert.Equal("16:41", intervalPace.TimeFormatted);
        Assert.True(intervalPace.HasPace);
        Assert.Equal("min/mi", intervalPace.PaceLabel);
        Assert.Equal("1:20", intervalPace.PaceValue);

        Assert.Equal(4, intervalDto.OverallRank);
        Assert.Equal(3, intervalDto.GenderRank);
        Assert.Equal(2, intervalDto.PrimaryDivisionRank);
        Assert.Equal(30, intervalDto.OverallCount);
        Assert.Equal(20, intervalDto.GenderCount);
        Assert.Equal(10, intervalDto.PrimaryDivisionCount);
        Assert.Equal(BetweenIntervalTimeIndicator.Improving, intervalDto.OverallIndicator);
        Assert.Equal(BetweenIntervalTimeIndicator.Improving, intervalDto.GenderIndicator);
        Assert.Equal(BetweenIntervalTimeIndicator.Improving, intervalDto.PrimaryDivisionIndicator);
        Assert.Equal("12:16:41 AM", intervalDto.CrossingTime);
        Assert.True(intervalDto.IsFullCourse);
    }

    #region test preperation methods

    private static MetadataGetTotalHelper GetMetadataGetTotalHelper()
    {
        var brackets = new List<Bracket>
        {
            new() { Id = 1, BracketType = BracketType.Overall },
            new() { Id = 2, BracketType = BracketType.Gender },
            new() { Id = 3, BracketType = BracketType.PrimaryDivision },
        };

        var bracketMetas = new List<BracketMetadata>
        {
            new() { BracketId = 1, TotalRacers = 30 },
            new() { BracketId = 2, TotalRacers = 20 },
            new() { BracketId = 3, TotalRacers = 10 },
        };

        return new MetadataGetTotalHelper(bracketMetas, brackets);
    }

    private static Course GetCourse()
    {
        return new()
        {
            Id = 1,
            Name = "CA",
            StartDate = new DateTime(2010, 1, 1),
            CourseType = CourseType.Running25K,
            PaceType = PaceType.MinuteMileOrKilometer,
            PreferedMetric = PreferredMetric.Imperial
        };
    }

    private static Result GetResult(int timeOnInterval = 1000)
    {
        return new()
        {
            IntervalId = 1,
            OverallRank = 4,
            GenderRank = 3,
            DivisionRank = 2,
            TimeOnCourse = 1001,
            TimeOnInterval = timeOnInterval
        };
    }

    private static Interval GetInterval(IntervalType intervalType, int distance = 10000, PaceType paceType = PaceType.None)
    {
        return new()
        {
            Id = 1,
            Name = "NA",
            IntervalType = intervalType,
            IsFullCourse = intervalType == IntervalType.FullCourse,
            Description = "DA",
            Distance = distance,
            DistanceFromStart = 20000,
            PaceType = paceType,
        };
    }

    #endregion
}
