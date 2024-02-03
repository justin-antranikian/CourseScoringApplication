using Core;
using Xunit;

namespace DomainTests;

public class PaceCalculatorTests
{
    [Fact]
    public void TestPace()
    {
        var paceValue = PaceCalculator.GetPace(PaceType.None, PreferedMetric.Metric, 3600, 1609.34);
        Assert.Null(paceValue);

        paceValue = PaceCalculator.GetPace(PaceType.MilesOrKilometersPerHour, PreferedMetric.Imperial, 3600, 1609.34);
        Assert.Equal(1, paceValue);

        paceValue = PaceCalculator.GetPace(PaceType.MilesOrKilometersPerHour, PreferedMetric.Imperial, 3600, 2414.016);
        Assert.Equal(1.5, paceValue);

        paceValue = PaceCalculator.GetPace(PaceType.MilesOrKilometersPerHour, PreferedMetric.Imperial, 3600, 1609.344);
        Assert.Equal(1, paceValue);

        paceValue = PaceCalculator.GetPace(PaceType.MilesOrKilometersPerHour, PreferedMetric.Imperial, 3600, 2414.0168);
        Assert.Equal(1.5, paceValue);

        paceValue = PaceCalculator.GetPace(PaceType.MilesOrKilometersPerHour, PreferedMetric.Imperial, 120, 3218.68);
        Assert.Equal(60, paceValue);

        paceValue = PaceCalculator.GetPace(PaceType.MilesOrKilometersPerHour, PreferedMetric.Metric, 3600, 1000);
        Assert.Equal(1, paceValue);

        paceValue = PaceCalculator.GetPace(PaceType.MilesOrKilometersPerHour, PreferedMetric.Metric, 3600, 1500);
        Assert.Equal(1.5, paceValue);

        paceValue = PaceCalculator.GetPace(PaceType.MilesOrKilometersPerHour, PreferedMetric.Metric, 120, 2000);
        Assert.Equal(60, paceValue);

        paceValue = PaceCalculator.GetPace(PaceType.MinuteMileOrKilometer, PreferedMetric.Imperial, 1200, 3218.68);
        Assert.Equal(10, paceValue);

        paceValue = PaceCalculator.GetPace(PaceType.MinuteMileOrKilometer, PreferedMetric.Imperial, 1260, 3218.68);
        Assert.Equal(10.5, paceValue);

        paceValue = PaceCalculator.GetPace(PaceType.MinuteMileOrKilometer, PreferedMetric.Metric, 1200, 2000);
        Assert.Equal(10, paceValue);

        paceValue = PaceCalculator.GetPace(PaceType.MinuteMileOrKilometer, PreferedMetric.Metric, 1260, 2000);
        Assert.Equal(10.5, paceValue);

        paceValue = PaceCalculator.GetPace(PaceType.MinuteMileOrKilometer, PreferedMetric.Imperial, 3600, 3218.68);
        Assert.Equal(30, paceValue);

        paceValue = PaceCalculator.GetPace(PaceType.MinuteMileOrKilometer, PreferedMetric.Metric, 3600, 2000);
        Assert.Equal(30, paceValue);

        paceValue = PaceCalculator.GetPace(PaceType.MinutePer100Meters, PreferedMetric.Imperial, 1200, 1000);
        Assert.Equal(2, paceValue);

        paceValue = PaceCalculator.GetPace(PaceType.MinutePer100Meters, PreferedMetric.Metric, 1200, 1000);
        Assert.Equal(2, paceValue);

        paceValue = PaceCalculator.GetPace(PaceType.MinutePer100Meters, PreferedMetric.Imperial, 1260, 1000);
        Assert.Equal(2.1, paceValue);

        paceValue = PaceCalculator.GetPace(PaceType.MinutePer100Meters, PreferedMetric.Imperial, 1320, 1000);
        Assert.Equal(2.2, paceValue);

        var paceValueAsString = PaceCalculator.GetPaceFormatted(PaceType.MinutePer100Meters, PreferedMetric.Imperial, 1260, 1000);
        Assert.Equal("2:06", paceValueAsString);

        paceValueAsString = PaceCalculator.GetPaceFormatted(PaceType.MinutePer100Meters, PreferedMetric.Imperial, 1320, 1000);
        Assert.Equal("2:12", paceValueAsString);

        paceValueAsString = PaceCalculator.GetPaceFormatted(PaceType.MinuteMileOrKilometer, PreferedMetric.Metric, 600, 1000);
        Assert.Equal("10:00", paceValueAsString);

        paceValueAsString = PaceCalculator.GetPaceFormatted(PaceType.MinuteMileOrKilometer, PreferedMetric.Imperial, 600, 1000);
        Assert.Equal("16:05", paceValueAsString);
    }
}
