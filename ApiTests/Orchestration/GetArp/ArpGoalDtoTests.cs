using Api.DataModels;
using Api.Orchestration.Athletes.GetDetails;

namespace ApiTests.Orchestration.GetArp;

public class ArpGoalDtoTests
{
    [Fact]
    public void MapsAllFields()
    {
        var courses = GetCourses();
        var arpGoalDto = ArpGoalDtoMapper.GetArpGoalDto(RaceSeriesType.Swim, 20, 10, 2000, courses);

        Assert.Equal("Swimming", arpGoalDto.RaceSeriesTypeName);
        Assert.Equal(20, arpGoalDto.GoalTotal);
        Assert.Equal(10, arpGoalDto.ActualTotal);
        Assert.Equal(2000, arpGoalDto.TotalDistance);
        Assert.Equal(50, arpGoalDto.PercentComplete);

        Assert.Collection(arpGoalDto.Courses, result =>
        {
            Assert.Equal(1, result.CourseId);
            Assert.Equal("CA", result.CourseName);
            Assert.Equal(1, result.RaceId);
            Assert.Equal("RA", result.RaceName);
            Assert.Equal("SA", result.RaceSeriesState);
            Assert.Equal("CA", result.RaceSeriesCity);
            Assert.Equal("DA", result.RaceSeriesDescription);
        });
    }

    [Fact]
    public void NoGoalsCompleted()
    {
        var courses = GetCourses();
        var arpGoalDto = ArpGoalDtoMapper.GetArpGoalDto(RaceSeriesType.Swim, 20, 0, 0, courses);

        Assert.Equal(0, arpGoalDto.PercentComplete);
    }

    [Fact]
    public void NoGoalTotalSet()
    {
        var courses = GetCourses();
        var arpGoalDto = ArpGoalDtoMapper.GetArpGoalDto(RaceSeriesType.Swim, 0, 1, 0, courses);

        Assert.Equal(100, arpGoalDto.PercentComplete);
    }

    #region test preperation methods

    private static List<Course> GetCourses()
    {
        var courses = new[]
        {
            new Course
            {
                Id = 1,
                Name = "CA",
                Race = new()
                {
                    Id = 1,
                    Name = "RA",
                    RaceSeries = new()
                    {
                        State = "SA",
                        City = "CA",
                        Description = "DA"
                    }
                },
                CourseType = CourseType.Running5K,
                Distance = 0,
                PaceType = PaceType.None,
                PreferedMetric = PreferredMetric.Metric,
                SortOrder = 0,
                StartDate = default
            }
        };

        return courses.ToList();
    }

    #endregion
}
