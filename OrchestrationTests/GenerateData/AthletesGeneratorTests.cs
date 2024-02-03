using Orchestration.GenerateData;
using Xunit;

namespace OrchestrationTests.GenerateData;

public class RaceSeriesGeneratorTests
{
    [Fact]
    public void RaceSeriesGenerator_ReturnsCorrectResults()
    {
        var raceSeriesEntries = RaceSeriesGenerator.GetRaceSeries().ToArray();

        Assert.Collection(raceSeriesEntries[0..4], raceSeries =>
        {
            Assert.Equal(1, raceSeries.OverallRank);
        }, raceSeries =>
        {
            Assert.Equal(2, raceSeries.OverallRank);
        }, raceSeries =>
        {
            Assert.Equal(3, raceSeries.OverallRank);
        }, raceSeries =>
        {
            Assert.Equal(4, raceSeries.OverallRank);
        });
    }
}
