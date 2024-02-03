using Core;
using Xunit;

namespace CoreTests;

public class RaceSeriesTypeExtensionsTests
{
    [Theory]
    [InlineData(RaceSeriesType.Running, "Running")]
    [InlineData(RaceSeriesType.Triathalon, "Triathalon")]
    [InlineData(RaceSeriesType.RoadBiking, "Road Biking")]
    [InlineData(RaceSeriesType.MountainBiking, "Mountain Biking")]
    [InlineData(RaceSeriesType.CrossCountrySkiing, "Cross Country Skiing")]
    [InlineData(RaceSeriesType.Swim, "Swimming")]
    public void ToFriendlyText_ReturnsCorrectResult(RaceSeriesType type, string expected)
    {
        Assert.Equal(expected, type.ToFriendlyText());
    }

    [Theory]
    [InlineData(RaceSeriesType.Running, "Runner")]
    [InlineData(RaceSeriesType.Triathalon, "Triathlete")]
    [InlineData(RaceSeriesType.RoadBiking, "Cyclist")]
    [InlineData(RaceSeriesType.MountainBiking, "Mountain Biker")]
    [InlineData(RaceSeriesType.CrossCountrySkiing, "Cross Country Skier")]
    [InlineData(RaceSeriesType.Swim, "Swimmer")]
    public void ToAthleteText_ReturnsCorrectResult(RaceSeriesType type, string expected)
    {
        Assert.Equal(expected, type.ToAthleteText());
    }
}
