using Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModels;

[Table("CourseTypeStatistics")]
public record CourseTypeStatistic
{
    [Key]
    public int Id { get; init; }

    public CourseType CourseType { get; init; }

    public int? AthleteId { get; init; }

    public int AverageTotalTimeInMilleseconds { get; init; }

    public int FastestTimeInMilleseconds { get; init; }

    public int SlowestTimeInMilleseconds { get; init; }
}
