using Api.DataModels;
using Api.DataModels.Enums;
using Api.Orchestration.CompareIrps;

namespace ApiTests.Orchestration.CompareIrps;

public class CompareIrpsOrchestratorTests
{
    [Fact]
    public async Task GetCompareIrpsResult_ReturnsCorrectResults()
    {
        var dbContext = ScoringDbContextCreator.GetEmptyDbContext();

        var course = new Course
        {
            Id = 1,
            Intervals = new()
            {
                new() { Id = 2, Name = "I2", Order = 2 },
                new() { Id = 1, Name = "I1", Order = 1 },
                new() { Id = 3, Name = "Full Course", Order = 3, IsFullCourse = true },
            },
            StartDate = new DateTime(2010, 1, 1, 7, 30, 0),
        };

        var secondAthleteResult = new Result { AthleteCourseId = 2, BracketId = 1, Rank = 1, TimeOnCourse = 1000, IsHighestIntervalCompleted = false };
        var thirdAthleteResult = new Result { AthleteCourseId = 3, BracketId = 1, Rank = 2, TimeOnCourse = 1001, IntervalId = 1, IsHighestIntervalCompleted = false };
        var fourthAthleteResult = new Result { AthleteCourseId = 4, BracketId = 1, Rank = 20, TimeOnCourse = 1002, IntervalId = 1, IsHighestIntervalCompleted = false, OverallRank = 20, GenderRank = 10, DivisionRank = 5 };
        var fifthAthleteResult = new Result { AthleteCourseId = 5, BracketId = 1, Rank = 21, TimeOnCourse = 1003, IntervalId = 1, IsHighestIntervalCompleted = false, OverallRank = 20, GenderRank = 10, DivisionRank = 5 };

        var athleteCourses = new List<AthleteCourse>
        {
            new()
            {
                Id = 2,
                Athlete = new()
                {
                    FullName = "FB",
                    Id = 20,
                    DateOfBirth = new DateTime(1983, 1, 1),
                    Gender = Gender.Femail,
                    State = "CO",
                    City = "Denver"
                },
                Results = new()
                {
                    secondAthleteResult with { IntervalId = 1, OverallRank = 1, GenderRank = 1, DivisionRank = 1 },
                    secondAthleteResult with { TimeOnCourse = 2000, IntervalId = 2 },
                    secondAthleteResult with { TimeOnCourse = 3000, IntervalId = 3 },
                    secondAthleteResult with { TimeOnCourse = 3000, IntervalId = 3, IsHighestIntervalCompleted = true },
                    secondAthleteResult with { BracketId = 45, TimeOnCourse = 3000, IntervalId = 3, IsHighestIntervalCompleted = true },
                },
                CourseId = 1,
                Bib = "A2"
            },
            new()
            {
                Id = 3,
                Athlete = new() { FullName = "FA", Id = 30 },
                Results = new()
                {
                    thirdAthleteResult with { BracketId = 45, Rank = 20, OverallRank = 2, GenderRank = 2, DivisionRank = 5 },
                    thirdAthleteResult with { OverallRank = 2, GenderRank = 2, DivisionRank = 2 },
                    thirdAthleteResult with { TimeOnCourse = 2001, IntervalId = 2 },
                    thirdAthleteResult with { TimeOnCourse = 3001, IntervalId = 3 },
                    thirdAthleteResult with { TimeOnCourse = 3001, IntervalId = 3, IsHighestIntervalCompleted = true },
                },
                CourseId = 1,
                Bib = "A1"
            },
            new()
            {
                Id = 4,
                Athlete = new() { FullName = "FC", Id = 40 },
                Results = new List<Result>
                {
                    fourthAthleteResult,
                    fourthAthleteResult with { IsHighestIntervalCompleted = true },
                },
                CourseId = 1,
                Bib = "A3"
            },
            new()
            {
                Id = 5,
                Athlete = new Athlete { FullName = "FD", Id = 50 },
                Results = new List<Result>
                {
                    fifthAthleteResult,
                    fifthAthleteResult with { IsHighestIntervalCompleted = true, OverallRank = 21, GenderRank = 11, DivisionRank = 6 },
                },
                CourseId = 1,
                Bib = "A4"
            }
        };

        var brackets = new List<Bracket>
        {
            new() { Id = 1, BracketType = BracketType.Overall, CourseId = course.Id }
        };

        await dbContext.Courses.AddRangeAsync(course);
        await dbContext.AthleteCourses.AddRangeAsync(athleteCourses);
        await dbContext.Brackets.AddRangeAsync(brackets);
        await dbContext.SaveChangesAsync();

        var compareIrpsOrchestrator = new CompareIrpsOrchestrator(dbContext);
        var idsToCompare = new[] { 2, 3, 4, 5 };
        var athleteInfoDtos = await compareIrpsOrchestrator.GetCompareIrpsDto(idsToCompare.ToList());

        Assert.Collection(athleteInfoDtos, result =>
        {
            Assert.Equal(20, result.AthleteId);
            Assert.Equal("FB", result.FullName);
            Assert.Equal(27, result.RaceAge);
            Assert.Equal("F", result.GenderAbbreviated);
            Assert.Equal("A2", result.Bib);
            Assert.Equal(CompareIrpsRank.First, result.CompareIrpsRank);
            Assert.Equal("CO", result.State);
            Assert.Equal("Denver", result.City);
            Assert.Equal(2, result.AthleteCourseId);
            Assert.Equal("8:20:00 AM", result.FinishInfo.FinishTime);
            Assert.Equal("50:00", result.FinishInfo.PaceWithTimeCumulative.TimeFormatted);
            Assert.Collection(result.CompareIrpsIntervalDtos, intervalResult =>
            {
                Assert.Equal("I1", intervalResult.IntervalName);
                Assert.Equal("16:40", intervalResult.PaceWithTime.TimeFormatted);
                Assert.Equal("7:46:40 AM", intervalResult.CrossingTime);
                Assert.Equal(1, intervalResult.OverallRank);
                Assert.Equal(1, intervalResult.GenderRank);
                Assert.Equal(1, intervalResult.PrimaryDivisionRank);
            }, intervalResult =>
            {
                Assert.Equal("33:20", intervalResult.PaceWithTime.TimeFormatted);
                Assert.Equal("8:03:20 AM", intervalResult.CrossingTime);
            }, intervalResult =>
            {
                Assert.Equal("50:00", intervalResult.PaceWithTime.TimeFormatted);
                Assert.Equal("8:20:00 AM", intervalResult.CrossingTime);
            });
        }, result =>
        {
            Assert.Equal(30, result.AthleteId);
            Assert.Equal("FA", result.FullName);
            Assert.Equal("A1", result.Bib);
            Assert.Equal(CompareIrpsRank.Second, result.CompareIrpsRank);
            Assert.Equal("8:20:01 AM", result.FinishInfo.FinishTime);
            Assert.Equal("50:01", result.FinishInfo.PaceWithTimeCumulative.TimeFormatted);
            Assert.Collection(result.CompareIrpsIntervalDtos, intervalResult =>
            {
                Assert.Equal("16:41", intervalResult.PaceWithTime.TimeFormatted);
                Assert.Equal("7:46:41 AM", intervalResult.CrossingTime);
                Assert.Equal(2, intervalResult.OverallRank);
                Assert.Equal(2, intervalResult.GenderRank);
                Assert.Equal(2, intervalResult.PrimaryDivisionRank);
            }, intervalResult =>
            {
                Assert.Equal("33:21", intervalResult.PaceWithTime.TimeFormatted);
                Assert.Equal("8:03:21 AM", intervalResult.CrossingTime);
            }, intervalResult =>
            {
                Assert.Equal("50:01", intervalResult.PaceWithTime.TimeFormatted);
                Assert.Equal("8:20:01 AM", intervalResult.CrossingTime);
            });
        }, result =>
        {
            Assert.Equal(40, result.AthleteId);
            Assert.Equal("FC", result.FullName);
            Assert.Equal("A3", result.Bib);
            Assert.Equal(CompareIrpsRank.Third, result.CompareIrpsRank);
            Assert.Null(result.FinishInfo);
            Assert.Collection(result.CompareIrpsIntervalDtos, intervalResult =>
            {
                Assert.Equal("16:42", intervalResult.PaceWithTime.TimeFormatted);
                Assert.Equal("7:46:42 AM", intervalResult.CrossingTime);
                Assert.Equal(20, intervalResult.OverallRank);
                Assert.Equal(10, intervalResult.GenderRank);
                Assert.Equal(5, intervalResult.PrimaryDivisionRank);
            }, intervalResult =>
            {
                Assert.Null(intervalResult.PaceWithTime);
            }, intervalResult =>
            {
                Assert.Null(intervalResult.PaceWithTime);
            });
        }, result =>
        {
            Assert.Equal(50, result.AthleteId);
            Assert.Equal("FD", result.FullName);
            Assert.Equal("A4", result.Bib);
            Assert.Equal(CompareIrpsRank.Fourth, result.CompareIrpsRank);
            Assert.Null(result.FinishInfo);
            Assert.Collection(result.CompareIrpsIntervalDtos, intervalResult =>
            {
                Assert.Equal("16:43", intervalResult.PaceWithTime.TimeFormatted);
                Assert.Equal("7:46:43 AM", intervalResult.CrossingTime);
            }, intervalResult =>
            {
                Assert.Null(intervalResult.PaceWithTime);
            }, intervalResult =>
            {
                Assert.Null(intervalResult.PaceWithTime);
            });
        });
    }
}
