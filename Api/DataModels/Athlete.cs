using Api.Orchestration;

namespace Api.DataModels;

public enum Gender
{
    Male,
    Femail
}

public class Athlete
{
    public int Id { get; set; }

    public required string Area { get; set; }
    public required int AreaRank { get; set; }
    public required string City { get; set; }
    public required int CityRank { get; set; }
    public required DateTime DateOfBirth { get; set; }
    public required string FirstName { get; set; }
    public required string FullName { get; set; }
    public required Gender Gender { get; set; }
    public required string LastName { get; set; }
    public required int OverallRank { get; set; }
    public required string State { get; set; }
    public required int StateRank { get; set; }

    public List<AthleteCourse> AthleteCourses { get; init; } = [];
    public List<AthleteRaceSeriesGoal> AthleteRaceSeriesGoals { get; init; } = [];
    public List<AthleteWellnessEntry> AthleteWellnessEntries { get; init; } = [];

    public int GetRaceAge(DateTime courseStartTime) => DateTimeHelper.GetRaceAge(DateOfBirth, courseStartTime);
}

public static class GenderExtensions
{
    public static string ToAbbreviation(this Gender gender)
    {
        return gender switch
        {
            Gender.Femail => "F",
            Gender.Male => "M",
            _ => throw new NotImplementedException()
        };
    }
}
