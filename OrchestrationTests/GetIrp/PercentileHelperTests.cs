using Orchestration.GetIrp;
using Xunit;

namespace OrchestrationTests.GetIrp;

public class PercentileHelperTests
{
	[Fact]
	public void PercentileHelper_FirstPlaceTest()
	{
		var percentile = PercentileHelper.GetPercentile(1, 10);

		Assert.Equal("1rst place", percentile);
	}

	[Fact]
	public void PercentileHelper_SecondPlaceTest()
	{
		var percentile = PercentileHelper.GetPercentile(2, 10);

		Assert.Equal("2nd place", percentile);
	}

	[Fact]
	public void PercentileHelper_ThirdPlaceTest()
	{
		var percentile = PercentileHelper.GetPercentile(3, 10);

		Assert.Equal("3rd place", percentile);
	}

	[Fact]
	public void PercentileHelper_FourthPlaceTest()
	{
		var percentile = PercentileHelper.GetPercentile(4, 10);

		Assert.Equal("40th percentile", percentile);
	}

	[Fact]
	public void PercentileHelper_LastPlaceTest()
	{
		var percentile = PercentileHelper.GetPercentile(10, 10);

		Assert.Equal("last place", percentile);
	}
}
