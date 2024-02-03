using Core;
using Xunit;

namespace CoreTests;

public class GenderExtensionsTests
{
    [Theory]
    [InlineData(Gender.Femail, "F")]
    [InlineData(Gender.Male, "M")]
    public void ToAbbreviation_ReturnsCorrectResults(Gender gender, string genderAbbreviated)
    {
        Assert.Equal(genderAbbreviated, gender.ToAbbreviation());
    }
}
