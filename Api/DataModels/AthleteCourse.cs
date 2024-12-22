namespace Api.DataModels;

public record AthleteCourse
{
    public int Id { get; set; }
    public int AthleteId { get; set; }
    public int CourseId { get; set; }

    public required string Bib { get; set; }
    public required string CourseGoalDescription { get; set; }
    public required string PersonalGoalDescription { get; set; }

    public Athlete? Athlete { get; set; }
    public List<AthleteCourseBracket> AthleteCourseBrackets { get; set; } = [];
    public List<AthleteCourseTraining> AthleteCourseTrainings { get; set; } = [];
    public Course? Course { get; set; }
    public List<Result> Results { get; set; } = [];
    public List<TagRead> TagReads { get; set; } = [];
}
