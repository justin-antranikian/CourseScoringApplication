using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModels;

public record Race
{
    public int Id { get; init; }
    public int RaceSeriesId { get; init; }

    public string Name { get; init; }
    public DateTime KickOffDate { get; init; }
    public string TimeZoneId { get; init; }

    public List<Course> Courses { get; set; } = [];
    public RaceSeries RaceSeries { get; init; }
}
