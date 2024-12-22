namespace Api.DataModels;

public record TagRead(int CourseId, int AthleteCourseId, int IntervalId, int TimeOnInterval, int TimeOnCourse)
{
    public int Id { get; init; }

    public Course Course { get; set; }
    public AthleteCourse AthleteCourse { get; set; }
    public Interval Interval { get; set; }
}
