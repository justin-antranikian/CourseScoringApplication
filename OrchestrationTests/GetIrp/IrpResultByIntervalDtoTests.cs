using Core;
using DataModels;
using Orchestration;
using Orchestration.GetIrp;
using System;
using System.Collections.Generic;
using Xunit;

namespace OrchestrationTests.GetIrp;

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
		Assert.Equal(IntervalType.Transition, intervalDto.IntervalType);
		Assert.False(intervalDto.IntervalFinished);

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
		Assert.Equal("DA", intervalDto.IntervalDescription);
		Assert.Null(intervalDto.Percentile);
		Assert.Equal(10000, intervalDto.IntervalDistance);
		Assert.Equal(20000, intervalDto.CumulativeDistance);
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
		Assert.Equal(IntervalType.Transition, intervalDto.IntervalType);
		Assert.True(intervalDto.IntervalFinished);

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
		Assert.Equal("DA", intervalDto.IntervalDescription);
		Assert.Equal("13th percentile", intervalDto.Percentile);
		Assert.Equal(10000, intervalDto.IntervalDistance);
		Assert.Equal(20000, intervalDto.CumulativeDistance);
	}

	[Fact]
	public void FullCourseInterval_IsMappedCorrectly()
	{
		var course = GetCourse();
		var interval = GetInterval(IntervalType.FullCourse, 20000, PaceType.MinuteMileOrKilometer);
		var metadataTotalHelper = GetMetadataGetTotalHelper();
		var result = GetResult(1001);

		var previousResult = new IrpResultByIntervalDto
		(
			"",
			IntervalType.MountainBike,
			true,
			null,
			null,
			10,
			10,
			10,
			0,
			0,
			0,
			BetweenIntervalTimeIndicator.GettingWorse,
			BetweenIntervalTimeIndicator.GettingWorse,
			BetweenIntervalTimeIndicator.GettingWorse,
			"",
			false,
			"",
			"",
			1000,
			1000
		);

		var intervalDto = IrpResultByIntervalDtoMapper.GetIrpResultByIntervalDto(course, interval, result, previousResult, metadataTotalHelper);

		Assert.NotNull(intervalDto);
		Assert.Equal("NA", intervalDto.IntervalName);
		Assert.Equal(IntervalType.FullCourse, intervalDto.IntervalType);
		Assert.True(intervalDto.IntervalFinished);

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
		Assert.Equal("DA", intervalDto.IntervalDescription);
		Assert.Equal("13th percentile", intervalDto.Percentile);
		Assert.Equal(20000, intervalDto.IntervalDistance);
		Assert.Equal(20000, intervalDto.CumulativeDistance);
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
			PreferedMetric = PreferedMetric.Imperial
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
