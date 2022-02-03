using Orchestration.GenerateData;
using Xunit;

namespace OrchestrationTests.GenerateData;

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
