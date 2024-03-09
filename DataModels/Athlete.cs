using Core;
using System;
using System.Collections.Generic;

namespace DataModels;

public record Athlete
{
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

    public List<AthleteCourse> AthleteCourses { get; init; }
    public List<AthleteRaceSeriesGoal> AthleteRaceSeriesGoals { get; init; } = [];
    public List<AthleteWellnessEntry> AthleteWellnessEntries { get; init; }

    public int GetRaceAge(DateTime courseStartTime) => DateTimeHelper.GetRaceAge(DateOfBirth, courseStartTime);
}

