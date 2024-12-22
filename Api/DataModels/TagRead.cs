namespace Api.DataModels;

public record TagRead
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public int AthleteCourseId { get; set; }
    public int IntervalId { get; set; }

    public required int TimeOnInterval { get; set; }
    public required int TimeOnCourse { get; set; }

    public Course? Course { get; set; }
    public AthleteCourse? AthleteCourse { get; set; }
    public Interval? Interval { get; set; }
}
