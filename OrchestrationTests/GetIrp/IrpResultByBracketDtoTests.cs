using DataModels;
using Orchestration.GetIrp;
using Xunit;

namespace OrchestrationTests.GetIrp;

public class IrpResultByBracketDtoTests
{
	[Fact]
	public void MapsAllFeilds()
	{
		var bracket = new Bracket
		{
			Name = "NA"
		};

		var result = new Result
		{
			OverallRank = 4,
			GenderRank = 3,
			DivisionRank = 2,
			Rank = 2
		};

		var bracketDto = IrpResultByBracketDtoMapper.GetIrpResultByBracketDto(bracket, result, 10);

		Assert.Equal("NA", bracketDto.Name);
		Assert.Equal(2, bracketDto.Rank);
		Assert.Equal(10, bracketDto.TotalRacers);
		Assert.Equal("2nd place", bracketDto.Percentile);
		Assert.Equal(80, bracketDto.DidBetterThenPercent);
		Assert.Equal(20, bracketDto.DidWorseThenPercent);
	}
}
