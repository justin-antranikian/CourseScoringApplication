using Core;
using System;
using Api.DataModels;
using Api.Orchestration.GetRaceSeriesDashboard;
using Xunit;

namespace OrchestrationTests.GetRaceSeriesDashboard;

public class RaceSeriesDashboardParticipantDtoTests
{
    [Fact]
    public void MapsAllFields()
    {
        var athleteCourse = GetAthleteCourse();

        var course = new Course
        {
            Id = 3,
            StartDate = new DateTime(2010, 1, 1)
        };

        var participantDto = RaceSeriesDashboardParticipantDtoMapper.GetDto(athleteCourse, course);

        Assert.Equal(1, participantDto.AthleteCourseId);
        Assert.Equal(2, participantDto.AthleteId);
        Assert.Equal("FA LA", participantDto.FullName);
        Assert.Equal("FA", participantDto.FirstName);
        Assert.Equal("BA", participantDto.Bib);
        Assert.Equal("SA", participantDto.State);
        Assert.Equal("CA", participantDto.City);
        Assert.Equal(10, participantDto.RaceAge);
        Assert.Equal("M", participantDto.GenderAbbreviated);
        Assert.Equal("CA", participantDto.CourseGoalDescription);
        Assert.Equal("PA", participantDto.PersonalGoalDescription);
        Assert.Equal(new[] { "DA" }, participantDto.TrainingList.ToArray());
    }

    #region test preperation methods

    private static AthleteCourse GetAthleteCourse()
    {
        return new()
        {
            Id = 1,
            AthleteId = 2,
            Bib = "BA",
            CourseGoalDescription = "CA",
            PersonalGoalDescription = "PA",
            Athlete = new()
            {
                Id = 2,
                FirstName = "FA",
                LastName = "LA",
                FullName = "FA LA",
                State = "SA",
                Area = "AA",
                City = "CA",
                DateOfBirth = new DateTime(2000, 1, 1),
                Gender = Gender.Male,
            },
            AthleteCourseTrainings = new()
            {
                new() { Description = "DA" }
            }
        };
    }

    #endregion
}
