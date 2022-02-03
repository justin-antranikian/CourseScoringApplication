using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModels;

[Table("BracketMetaDatas")]
public class BracketMetadata
{
	[Key]
	public int Id { get; set; }

	public int CourseId { get; set; }

	public int BracketId { get; set; }

	public int TotalRacers { get; set; }

	public int? IntervalId { get; set; }

	public BracketMetadata() { }

	public BracketMetadata(int courseId, int bracketId, int totalRacers, int? intervalId = null)
	{
		CourseId = courseId;
		BracketId = bracketId;
		TotalRacers = totalRacers;
		IntervalId = intervalId;
	}
}
