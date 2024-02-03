using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModels;

[Table("Races")]
public record Race
{
    [Key]
    public int Id { get; init; }

    public string Name { get; init; }

    public int RaceSeriesId { get; init; }

    public RaceSeries RaceSeries { get; init; }

    public DateTime KickOffDate { get; init; }

    public string TimeZoneId { get; init; }

    [ForeignKey("RaceId")]
    public List<Course> Courses { get; set; } = new();
}
