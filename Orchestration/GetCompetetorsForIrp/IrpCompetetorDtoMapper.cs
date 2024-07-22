using DataModels.Extensions;

namespace Orchestration.GetCompetetorsForIrp;

public static class IrpCompetetorDtoMapper
{
    public static IrpCompetetorDto GetIrpCompetetorDto(Result result, Course course, Interval highestIntervalCompleted)
    {
        var athleteCourse = result.AthleteCourse;
        var athlete = athleteCourse.Athlete;
        var paceWithTimeCumulative = course.GetPaceWithTime(result.TimeOnCourse, highestIntervalCompleted.DistanceFromStart);

        return new
        (
            athlete.Id,
            athlete.FullName,
            athlete.DateOfBirth,
            athlete.Gender,
            athleteCourse.Bib,
            course.StartDate,
            paceWithTimeCumulative,
            athleteCourse.Id,
            athlete.FirstName
        );
    }
}
