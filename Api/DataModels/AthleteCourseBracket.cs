namespace Api.DataModels;

public class AthleteCourseBracket(int athleteCourseId, int courseId, int bracketId)
{
    public int Id { get; set; }
    public int AthleteCourseId { get; set; } = athleteCourseId;
    public int BracketId { get; set; } = bracketId;
    public int CourseId { get; set; } = courseId;

    public AthleteCourse AthleteCourse { get; set; }
    public Bracket Bracket { get; set; }
    public Course Course { get; set; }
}
