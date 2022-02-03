using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModels;

[Table("AthleteCourseTrainings")]
public class AthleteCourseTraining
{
	[Key]
	public int Id { get; set; }

	public int AthleteCourseId { get; set; }

	public string Description { get; set; }
}
