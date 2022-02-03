using System.ComponentModel.DataAnnotations.Schema;

namespace DataModels;

[Table("Results")]
public record Result : ResultBase
{
	public int OverallRank { get; init; }

	public int GenderRank { get; init; }

	public int DivisionRank { get; init; }

	public bool IsHighestIntervalCompleted { get; init; }
}
