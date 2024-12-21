using Core;

namespace Api.DataModels;

public record Athlete
{
    public int Id { get; init; }

    public string Area { get; init; }
    public int AreaRank { get; set; }
    public string City { get; init; }
    public int CityRank { get; set; }
    public DateTime DateOfBirth { get; init; }
    public Gender Gender { get; init; }
    public string FirstName { get; init; }
    public string FullName { get; init; }
    public string LastName { get; init; }
    public int OverallRank { get; set; }
    public string State { get; init; }
    public int StateRank { get; set; }

    public List<AthleteCourse> AthleteCourses { get; set; } = [];
    public List<AthleteRaceSeriesGoal> AthleteRaceSeriesGoals { get; set; } = [];
    public List<AthleteWellnessEntry> AthleteWellnessEntries { get; set; } = [];

    public int GetRaceAge(DateTime courseStartTime) => DateTimeHelper.GetRaceAge(DateOfBirth, courseStartTime);
}

