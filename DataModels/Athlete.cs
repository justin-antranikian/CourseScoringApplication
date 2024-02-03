using Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModels;

[Table("Athletes")]
public record Athlete
{
    [Key]
    public int Id { get; init; }

    public string FirstName { get; init; }

    public string LastName { get; init; }

    public string FullName { get; init; }

    public DateTime DateOfBirth { get; init; }

    public string City { get; init; }

    public string Area { get; init; }

    public string State { get; init; }

    public Gender Gender { get; init; }

    public int OverallRank { get; set; }

    public int StateRank { get; set; }

    public int AreaRank { get; set; }

    public int CityRank { get; set; }

    [ForeignKey("AthleteId")]
    public List<AthleteCourse> AthleteCourses { get; init; }

    [ForeignKey("AthleteId")]
    public List<AthleteRaceSeriesGoal> AthleteRaceSeriesGoals { get; init; } = new();

    [ForeignKey("AthleteId")]
    public List<AthleteWellnessEntry> AthleteWellnessEntries { get; init; }

    [ForeignKey("AthleteFromId")]
    public List<AthleteRelationshipEntry> AthleteRelationshipEntries { get; set; }
}

