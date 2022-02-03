using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModels;

[Table("AthleteCourses")]
public record AthleteCourse
{
	[Key]
	public int Id { get; init; }

	public int AthleteId { get; init; }

	public int CourseId { get; init; }

	public string Bib { get; init; }

	public string CourseGoalDescription { get; init; }

	public string PersonalGoalDescription { get; init; }

	public Athlete Athlete { get; init; }

	public Course Course { get; init; }

	[ForeignKey("AthleteCourseId")]
	public List<Result> Results { get; init; } = new();

	[ForeignKey("AthleteCourseId")]
	public List<AthleteCourseBracket> AthleteCourseBrackets { get; init; } = new();

	[ForeignKey("AthleteCourseId")]
	public List<AthleteCourseTraining> AthleteCourseTrainings { get; init; } = new();
}
