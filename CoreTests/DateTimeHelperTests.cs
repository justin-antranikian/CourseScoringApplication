using Core;
using System;
using Xunit;

namespace CoreTests;

public class DateTimeHelperTests
{
    [Theory]
    [InlineData(10, 20, 10, 19, 36)]
    [InlineData(10, 20, 10, 20, 37)]
    [InlineData(10, 20, 10, 21, 37)]
    [InlineData(10, 20, 9, 20, 36)]
    [InlineData(10, 20, 11, 20, 37)]
    public void GetRaceAge_ReturnsCorrectResult(params int[] values)
    {
        var dateOfBirth = new DateTime(1983, values[0], values[1]);
        var courseStartTime = new DateTime(2020, values[2], values[3]);
        Assert.Equal(values[4], DateTimeHelper.GetRaceAge(dateOfBirth, courseStartTime));
    }

    [Fact]
    public void GetCurrentAge_ReturnsCorrectResult()
    {
        var dateOfBirth = new DateTime(1983, 1, 1);
        var currentAge = DateTimeHelper.GetCurrentAge(dateOfBirth);
        Assert.True(currentAge >= 36); // as time goes, the current age will increase. However the test will still pass.
    }

    [Fact]
    public void GetFormattedFields_ReturnsCorrectResult()
    {
        var dateOfBirth = new DateTime(2010, 1, 1, 6, 30, 0);
        var (dateFormatted, timeFormatted) = DateTimeHelper.GetFormattedFields(dateOfBirth);
        Assert.Equal("1/1/2010", dateFormatted);
        Assert.Equal("06:30:00 AM", timeFormatted);
    }

    [Fact]
    public void GetCrossingTime_ReturnsCorrectResult()
    {
        var courseStartTime = new DateTime(2010, 1, 1, 6, 30, 0);
        var crossingTime = DateTimeHelper.GetCrossingTime(courseStartTime, 1000);
        Assert.Equal("6:46:40 AM", crossingTime);
    }
}
