using Api.DataModels;
using Api.Orchestration.SearchAthletes;

namespace ApiTests.Orchestration.SearchAthletes;

public class AthleteSearchResultDtoTests
{
    [Fact]
    public void MapsAllFields()
    {
        var athlete = new Athlete
        {
            Id = 1,
            FullName = "FA LA",
            DateOfBirth = new DateTime(2010, 1, 1),
            Gender = Gender.Male,
            State = "SA",
            Area = "AA",
            City = "CA",
            OverallRank = 4,
            AthleteRaceSeriesGoals = new()
            {
                new (RaceSeriesType.Triathalon, 15),
                new (RaceSeriesType.Running, 15),
            }
        };

        var athleteDto = AthleteSearchResultDtoMapper.GetAthleteSearchResultDto(athlete);

        Assert.Equal(1, athleteDto.Id);
        Assert.Equal("FA LA", athleteDto.FullName);
        Assert.True(athleteDto.Age >= 10);
        Assert.Equal("M", athleteDto.GenderAbbreviated);
        Assert.Equal(4, athleteDto.LocationInfoWithRank.OverallRank);
        Assert.Equal(new[] { "Triathlete", "Runner" }, athleteDto.Tags);
    }
}
