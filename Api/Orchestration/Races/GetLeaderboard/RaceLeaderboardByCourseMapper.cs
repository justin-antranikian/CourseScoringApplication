using Api.DataModels;

namespace Api.Orchestration.Races.GetLeaderboard;

internal class RaceLeaderboardByCourseMapper(List<Interval> highestCompletedIntervalsForAllCourses, List<Result> resultsForAllCourses)
{
    /// <summary>
    /// Takes a list a list of courses and returns a list of TopThreeAthletesForCourseDtos.
    /// </summary>
    /// <param name="courses"></param>
    /// <returns></returns>
    public IEnumerable<RaceLeaderboardByCourseDto> GetResults(List<Course> courses)
    {
        foreach (var course in courses)
        {
            var interval = highestCompletedIntervalsForAllCourses.Single(oo => oo.CourseId == course.Id);
            var leaderboardResultDtos = GetLeaderboardResultDtos(course, interval).ToList();

            yield return new RaceLeaderboardByCourseDto
            {
                CourseId = course.Id,
                CourseName = course.Name,
                SortOrder = interval.Order,
                Results = leaderboardResultDtos
            };
        }
    }

    private IEnumerable<LeaderboardResultDto> GetLeaderboardResultDtos(Course course, Interval intervalForCourse)
    {
        var resultsForCourse = resultsForAllCourses.Where(oo => oo.IntervalId == intervalForCourse.Id);

        foreach (var result in resultsForCourse)
        {
            var distanceCompleted = intervalForCourse.DistanceFromStart;
            var timeOnCoursePace = course.GetPaceWithTime(result.TimeOnCourse, distanceCompleted);
            var athlete = result.AthleteCourse.Athlete;
            yield return LeaderboardResultDtoMapper.GetLeaderboardResultDto(result, athlete, timeOnCoursePace, course);
        }
    }
}
