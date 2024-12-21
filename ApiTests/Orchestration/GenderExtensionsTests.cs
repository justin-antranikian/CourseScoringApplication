using Api.DataModels.Enums;

namespace ApiTests.Orchestration;

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
