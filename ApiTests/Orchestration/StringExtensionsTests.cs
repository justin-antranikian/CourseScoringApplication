using Api.Orchestration;

namespace ApiTests.Orchestration;

public class StringExtensionsTests
{
    [Theory]
    [InlineData("denver", "denver")]
    [InlineData("Denver", "denver")]
    [InlineData("Greater Denver", "greater-denver")]
    public void ToUrlFriendlyText_ReturnsCorrectResult(string unformattedText, string expected)
    {
        Assert.Equal(expected, unformattedText.ToUrlFriendlyText());
    }
}
