using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Orchestration.GetRaceSeriesDashboard;

public class GetRaceSeriesDashboardOrchestrator
{
    private readonly ScoringDbContext _scoringDbContext;

    public GetRaceSeriesDashboardOrchestrator(ScoringDbContext scoringDbContext)
    {
        _scoringDbContext = scoringDbContext;
    }

    public async Task<RaceSeriesDashboardDto> GetRaceSeriesDashboardDto(int raceSeriesId)
    {
        var raceSeries = await GetRaceSeries(raceSeriesId);
        var orderedRaces = raceSeries.Races.OrderByDescending(oo => oo.KickOffDate).ToList();
        var upcomingRace = orderedRaces.First();
        var athleteCourses = await GetAthleteCourses(upcomingRace.Id);

        RaceSeriesDashboardCourseDto MapCourseDto(Course course)
        {
            var courseId = course.Id;
            var athletesForCourse = athleteCourses.Where(oo => oo.CourseId == courseId);
            var participantsForCourse = athletesForCourse.Select(oo => RaceSeriesDashboardParticipantDtoMapper.GetDto(oo, course)).ToList();
            var courseDto = new RaceSeriesDashboardCourseDto(course.Id, course.Name, course.CourseInformationEntries.ToList(), participantsForCourse);
            return courseDto;
        }

        var courses = upcomingRace.Courses.OrderBy(oo => oo.SortOrder).Select(MapCourseDto).ToList();
        var races = orderedRaces.Select(oo => new PastRaceDto(oo.Id, oo.Name, oo.KickOffDate.ToShortDateString())).ToList();
        return RaceSeriesDashboardDtoMapper.GetRaceSeriesDashboardDto(raceSeries, races, courses);
    }

    private async Task<RaceSeries> GetRaceSeries(int raceSeriesId)
    {
        return await _scoringDbContext.RaceSeries
                        .Include(oo => oo.Races)
                        .ThenInclude(oo => oo.Courses)
                        .ThenInclude(oo => oo.CourseInformationEntries)
                        .SingleAsync(oo => oo.Id == raceSeriesId);
    }

    private async Task<List<AthleteCourse>> GetAthleteCourses(int upcomingRaceId)
    {
        var query = _scoringDbContext.AthleteCourses
                        .Include(oo => oo.AthleteCourseTrainings)
                        .Include(oo => oo.Athlete)
                        .Where(oo => oo.Course.RaceId == upcomingRaceId);

        return await query.ToListAsync();
    }
}
