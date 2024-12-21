using Api.DataModels;
using Api.DataModels.Enums;
using Api.Orchestration.GetRaceSeriesDashboard;

namespace ApiTests.Orchestration.GetRaceSeriesDashboard;

public class RaceSeriesDashboardCourseDtoTests
{
    private static readonly CourseInformationType _description = CourseInformationType.Description;
    private static readonly CourseInformationType _promo = CourseInformationType.Promotional;
    private static readonly CourseInformationType _howTo = CourseInformationType.HowToPrepare;

    [Fact]
    public void MapsAllFields()
    {
        var informationEntries = GetCourseInformationEntries();
        var courseDto = new RaceSeriesDashboardCourseDto(1, "A", informationEntries.ToList(), new());

        Assert.Equal(1, courseDto.Id);
        Assert.Equal("A", courseDto.DisplayName);

        AssertInformationEntries(courseDto.DescriptionEntries, _description, "D1", "D2");
        AssertInformationEntries(courseDto.PromotionalEntries, _promo, "P1");
        AssertInformationEntries(courseDto.HowToPrepareEntries, _howTo, "H1");

        Assert.Empty(courseDto.Participants);
    }

    private static void AssertInformationEntries(List<CourseInformationEntryDto> courseInfos, CourseInformationType type, params string[] descriptions)
    {
        var count = 0;
        foreach (var courseInfo in courseInfos)
        {
            var description = descriptions[count];
            Assert.Equal(type, courseInfo.CourseInformationType);
            Assert.Equal(description, courseInfo.Description);
            count++;
        }
    }

    #region test preperation methods

    private static List<CourseInformationEntry> GetCourseInformationEntries()
    {
        return new()
        {
            new (_description, "D1"),
            new (_description, "D2"),
            new (_promo, "P1"),
            new (_howTo, "H1"),
        };
    }

    #endregion
}
