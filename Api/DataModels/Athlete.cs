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
    public int AreaLocationId { get; set; }
    public int CityLocationId { get; set; }
    public int StateLocationId { get; set; }

    public required int AreaRank { get; set; }
    public required int CityRank { get; set; }
    public required DateTime DateOfBirth { get; set; }
    public required string FirstName { get; set; }
    public required string FullName { get; set; }
    public required Gender Gender { get; set; }
    public required string LastName { get; set; }
    public required int OverallRank { get; set; }
    public required int StateRank { get; set; }

    public Location AreaLocation { get; set; }
    public Location CityLocation { get; set; }
    public Location StateLocation { get; set; }
    public List<AthleteCourse> AthleteCourses { get; init; } = [];
    public List<AthleteRaceSeriesGoal> AthleteRaceSeriesGoals { get; init; } = [];
    public List<AthleteWellnessEntry> AthleteWellnessEntries { get; init; } = [];

    public int GetRaceAge(DateTime courseStartTime) => DateTimeHelper.GetRaceAge(DateOfBirth, courseStartTime);

    public string GetGenderFormatted() => Gender.ToAbbreviation();

    public LocationInfoWithRank ToLocationInfoWithRank()
    {
        return new LocationInfoWithRank
        {
            Area = AreaLocation!.Name,
            AreaRank = AreaRank,
            AreaUrl = AreaLocation.Slug,
            City = CityLocation!.Name,
            CityRank = CityRank,
            CityUrl = CityLocation.Slug,
            OverallRank = OverallRank,
            State = StateLocation!.Name,
            StateRank = StateRank,
            StateUrl = StateLocation.Slug,
        };
    }
}

public static class GenderExtensions
{
    public static string ToAbbreviation(this Gender gender)
    {
        return gender switch
        {
            Gender.Femail => "F",
            Gender.Male => "M",
        };
    }
}
