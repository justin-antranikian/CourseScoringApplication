using Api.Orchestration.GenerateData;

namespace ApiTests.Orchestration.GenerateData;

public class AthletesGeneratorTests
{
    [Fact]
    public void AthletesGenerator_ReturnsCorrectResults()
    {
        var athletes = AthletesGenerator.GetAthletes().ToArray();

        Assert.Collection(athletes[0..4], result =>
        {
            Assert.Equal(0, result.OverallRank);
        }, result =>
        {
            Assert.Equal(0, result.OverallRank);
        }, result =>
        {
            Assert.Equal(0, result.OverallRank);
        }, result =>
        {
            Assert.Equal(0, result.OverallRank);
        });
    }
}
