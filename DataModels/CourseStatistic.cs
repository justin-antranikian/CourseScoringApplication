using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModels;

[Table("CourseStatistics")]
public record CourseStatistic
{
	[Key]
	public int Id { get; init; }

	public int CourseId { get; init; }

	public int? BracketId { get; init; }

	public int? IntervalId { get; init; }

	public int AverageTotalTimeInMilleseconds { get; init; }

	public int FastestTimeInMilleseconds { get; init; }

	public int SlowestTimeInMilleseconds { get; init; }
}
