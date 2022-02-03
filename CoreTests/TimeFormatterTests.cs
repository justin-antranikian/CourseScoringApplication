using Core;
using Xunit;

namespace DomainTests;

public class TimeFormatterTests
{
	[Fact]
	public void TestTimeFormatter()
	{
		Assert.Equal("00:01", TimeFormatter.Format(1));
		Assert.Equal("00:11", TimeFormatter.Format(11));
		Assert.Equal("1:01", TimeFormatter.Format(61));
		Assert.Equal("1:11", TimeFormatter.Format(71));

		Assert.Equal("1:00:01", TimeFormatter.Format(3601));
		Assert.Equal("1:00:11", TimeFormatter.Format(3611));
		Assert.Equal("1:01:01", TimeFormatter.Format(3661));
		Assert.Equal("1:01:11", TimeFormatter.Format(3671));

		Assert.Equal("1:00:59", TimeFormatter.Format(3659));
		Assert.Equal("1:01:00", TimeFormatter.Format(3660));
	}
}
