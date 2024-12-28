using Api.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Api.Orchestration.Courses.GetDetails;

public class GetDetailsOrchestrator(ScoringDbContext dbContext)
{
    public async Task<CourseDetailsDto> Get(int courseId)
    {
        var course = await dbContext.Courses.Include(oo => oo.Race).SingleAsync(oo => oo.Id == courseId);
        var raceSeriesId = course.Race.RaceSeriesId;

        var raceSeries = await dbContext.RaceSeries
            .Include(oo => oo.StateLocation)
            .Include(oo => oo.AreaLocation)
            .Include(oo => oo.CityLocation)
            .SingleAsync(oo => oo.Id == raceSeriesId);

        return MapToDto(course, raceSeries);
    }

    private static CourseDetailsDto MapToDto(Course course, RaceSeries raceSeries)
    {
        return new CourseDetailsDto
        {
            CourseId = course.Id,
            CourseName = course.Name,
            CourseType = course.CourseType.ToString(),
            Distance = course.Distance,
            Name = course.Name,
            PaceType = course.PaceType.ToString(),
            PreferedMetric = course.PreferedMetric.ToString(),
            SortOrder = course.SortOrder,
            RaceId = course.RaceId,
            RaceName = course.Race.Name,
            LocationInfoWithRank = raceSeries.ToLocationInfoWithRank()
        };
    }
}