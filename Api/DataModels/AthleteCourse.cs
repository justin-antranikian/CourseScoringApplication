namespace Api.DataModels;

public class AthleteCourse
{
    public int Id { get; set; }
    public int AthleteId { get; set; }
    public int CourseId { get; set; }

    public required string Bib { get; set; }
    public required string CourseGoalDescription { get; set; }
    public required string PersonalGoalDescription { get; set; }

    public Athlete Athlete { get; set; }
    public List<AthleteCourseBracket> AthleteCourseBrackets { get; init; } = [];
    public List<AthleteCourseTraining> AthleteCourseTrainings { get; init; } = [];
    public Course Course { get; set; }
    public List<Result> Results { get; init; } = [];
    public List<TagRead> TagReads { get; init; } = [];
}
