using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModels;

[Table("TagReads")]
public record TagRead
{
    [Key]
    public int Id { get; init; }

    public int CourseId { get; init; }

    public int AthleteCourseId { get; init; }

    public int IntervalId { get; init; }

    public int TimeOnInterval { get; init; }

    public int TimeOnCourse { get; init; }

    public TagRead(int courseId, int athleteCourseId, int intervalId, int timeOnInterval, int timeOnCourse)
    {
        CourseId = courseId;
        AthleteCourseId = athleteCourseId;
        IntervalId = intervalId;
        TimeOnInterval = timeOnInterval;
        TimeOnCourse = timeOnCourse;
    }
}
