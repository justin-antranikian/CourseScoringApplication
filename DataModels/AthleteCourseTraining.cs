namespace DataModels;

public class AthleteCourseTraining
{
    public int Id { get; set; }
    public int AthleteCourseId { get; set; }

    public required string Description { get; set; }

    public AthleteCourse AthleteCourse { get; set; }
}
