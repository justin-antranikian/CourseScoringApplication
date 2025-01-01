using Api.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Api.Orchestration.Courses.GetDetails;

public class GetDetailsOrchestrator(ScoringDbContext dbContext)
{
    public async Task<CourseDetailsDto> Get(int courseId)
    {
        var course = await dbContext.Courses.Include(oo => oo.Race).SingleAsync(oo => oo.Id == courseId);
        var raceSeriesId = course.Race.RaceSeriesId;
        var raceSeries = await dbContext.GetRaceSeriesWithLocationInfo().SingleAsync(oo => oo.Id == raceSeriesId);
        return MapToDto(course, raceSeries);
    }

    private static CourseDetailsDto MapToDto(Course course, RaceSeries raceSeries)
    {
        var (dateFormatted, timeFormatted) = DateTimeHelper.GetFormattedFields(course.StartDate);

        return new CourseDetailsDto
        {
            Id = course.Id,
            CourseDate = dateFormatted,
            CourseTime = timeFormatted,
            Distance = course.Distance,
            LocationInfoWithRank = raceSeries.ToLocationInfoWithRank(),
            Name = course.Name,
            RaceId = course.RaceId,
            RaceName = course.Race.Name,
            RaceSeriesType = raceSeries.RaceSeriesType.ToString()
        };
    }
}