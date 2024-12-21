namespace Api.DataModels;

public record AthleteCourse
{
    public int Id { get; init; }
    public int AthleteId { get; init; }
    public int CourseId { get; init; }

    public string Bib { get; init; }
    public string CourseGoalDescription { get; init; }
    public string PersonalGoalDescription { get; init; }

    public Athlete Athlete { get; init; }
    public List<AthleteCourseBracket> AthleteCourseBrackets { get; init; } = [];
    public List<AthleteCourseTraining> AthleteCourseTrainings { get; init; } = [];
    public Course Course { get; init; }
    public List<Result> Results { get; init; } = [];
    public List<TagRead> TagReads { get; set; } = [];
}
