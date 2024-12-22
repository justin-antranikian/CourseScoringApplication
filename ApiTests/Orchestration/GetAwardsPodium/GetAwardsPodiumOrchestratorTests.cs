using Api.DataModels;
using Api.Orchestration.Courses.GetAwards;

namespace ApiTests.Orchestration.GetAwardsPodium;

public class GetAwardsOrchestratorTests
{
    [Fact]
    public async Task MapsAllFields()
    {
        var dbContext = ScoringDbContextCreator.GetEmptyDbContext();

        var course = new Course
        {
            Id = 1,
            StartDate = new DateTime(2010, 1, 1, 7, 30, 0),
            CourseType = CourseType.Running5K,
            Distance = 0,
            Name = "",
            PaceType = PaceType.None,
            PreferedMetric = PreferredMetric.Metric,
            SortOrder = 0,
        };

        var intervals = new List<Interval>
        {
            new() { Id = 55, CourseId = 1, IntervalType = IntervalType.FullCourse, IsFullCourse = true, Name = "", Distance = 0, DistanceFromStart = 0, Order = 0, PaceType = PaceType.None },
            new() { Id = 56, CourseId = 1, IntervalType = IntervalType.Bike, IsFullCourse = false, Name = "", Distance = 0, DistanceFromStart = 0, Order = 0, PaceType = PaceType.None }
        };

        var athleteCourses = Enumerable.Range(1, 50).Select(index => new AthleteCourse
        {
            Id = index,
            Bib = "",
            CourseGoalDescription = "",
            PersonalGoalDescription = "",
            Athlete = new Athlete
            {
                Id = index,
                FullName = $"JA{index}",
                Area = "",
                City = "",
                State = "",
                FirstName = "",
                LastName = "",
                AreaRank = 0,
                CityRank = 0,
                DateOfBirth = default,
                Gender = Gender.Male,
                OverallRank = 0,
                StateRank = 0
            }
        }).ToList();

        var overallBracket = new Bracket { CourseId = 1, BracketType = BracketType.Overall, Id = 5, Name = "Overall" };

        var brackets = new List<Bracket>
        {
            overallBracket,
            overallBracket with { BracketType = BracketType.Gender, Id = 6, Name = "Male" },
            overallBracket with { BracketType = BracketType.Gender, Id = 7, Name = "Female" },
            overallBracket with { BracketType = BracketType.PrimaryDivision, Id = 8, Name = "8 Division" },
            overallBracket with { BracketType = BracketType.PrimaryDivision, Id = 9, Name = "9 Division" },
            overallBracket with { BracketType = BracketType.NonPrimaryDivision, Id = 10, Name = "10 Division" },
            overallBracket with { BracketType = BracketType.NonPrimaryDivision, Id = 11, Name = "11 Division" },
            overallBracket with { BracketType = BracketType.PrimaryDivision, Id = 12, Name = "12 Division" },
            overallBracket with { BracketType = BracketType.PrimaryDivision, Id = 13, Name = "13 Division" },
        };

        var baseResult = new Result
        {
            CourseId = 1,
            IntervalId = 55,
            DivisionRank = 0,
            GenderRank = 0,
            IsHighestIntervalCompleted = false,
            OverallRank = 0,
            AthleteCourseId = 0,
            BracketId = 0,
            Rank = 0,
            TimeOnInterval = 0,
            TimeOnCourse = 0
        };
        var firstAthleteCourseResult = baseResult with { AthleteCourseId = 1, BracketId = 5, Rank = 1, TimeOnCourse = 1000 };

        var results = new List<Result>
        {
            firstAthleteCourseResult,
            firstAthleteCourseResult with { BracketId = 6 },
            firstAthleteCourseResult with { BracketId = 8 },
            firstAthleteCourseResult with { BracketId = 10 },
            baseResult with { AthleteCourseId = 2, BracketId = 5, Rank = 2, },
            baseResult with { AthleteCourseId = 2, BracketId = 6, Rank = 2, },
            baseResult with { AthleteCourseId = 2, BracketId = 9, Rank = 1, },
            baseResult with { AthleteCourseId = 2, BracketId = 11, Rank = 1,},
            baseResult with { AthleteCourseId = 3, BracketId = 5, Rank = 3, },
            baseResult with { AthleteCourseId = 3, BracketId = 7, Rank = 1, },
            baseResult with { AthleteCourseId = 3, BracketId = 12, Rank = 1, },
            baseResult with { AthleteCourseId = 4, BracketId = 5, Rank = 4, },
            baseResult with { AthleteCourseId = 4, BracketId = 7, Rank = 2, },
            baseResult with { AthleteCourseId = 4, BracketId = 13, Rank = 1, },
            baseResult with { AthleteCourseId = 5, BracketId = 5, Rank = 5, },
            baseResult with { AthleteCourseId = 5, BracketId = 6, Rank = 3, },
            baseResult with { AthleteCourseId = 5, BracketId = 8, Rank = 2, },
            baseResult with { AthleteCourseId = 6, BracketId = 5, Rank = 6, },
            baseResult with { AthleteCourseId = 6, BracketId = 6, Rank = 4, },
            baseResult with { AthleteCourseId = 6, BracketId = 8, Rank = 3, },
            baseResult with { AthleteCourseId = 7, BracketId = 5, Rank = 7, },
            baseResult with { AthleteCourseId = 7, BracketId = 6, Rank = 5, },
            baseResult with { AthleteCourseId = 7, BracketId = 8, Rank = 4, },
            baseResult with { AthleteCourseId = 8, BracketId = 5, Rank = 8, },
            baseResult with { AthleteCourseId = 8, BracketId = 6, Rank = 6, },
            baseResult with { AthleteCourseId = 8, BracketId = 8, Rank = 5, },
            baseResult with { AthleteCourseId = 9, BracketId = 5, Rank = 9, },
            baseResult with { AthleteCourseId = 9, BracketId = 7, Rank = 3, },
            baseResult with { AthleteCourseId = 9, BracketId = 13, Rank = 2, },
            baseResult with { AthleteCourseId = 10, BracketId = 5, Rank = 10, },
            baseResult with { AthleteCourseId = 10, BracketId = 6, Rank = 7, },
            baseResult with { AthleteCourseId = 10, BracketId = 8, Rank = 6, },
            baseResult with { AthleteCourseId = 11, BracketId = 5, Rank = 12, },
            baseResult with { AthleteCourseId = 11, BracketId = 6, Rank = 9, },
            baseResult with { AthleteCourseId = 11, BracketId = 8, Rank = 8, },
            baseResult with { AthleteCourseId = 12, BracketId = 5, Rank = 11, },
            baseResult with { AthleteCourseId = 12, BracketId = 6, Rank = 8, },
            baseResult with { AthleteCourseId = 12, BracketId = 8, Rank = 7, },
        };

        results.ForEach(result =>
        {
            result.AthleteCourse = athleteCourses.Single(oo => oo.Id == result.AthleteCourseId);
        });

        await dbContext.Courses.AddAsync(course);
        await dbContext.Intervals.AddRangeAsync(intervals);
        await dbContext.Brackets.AddRangeAsync(brackets);
        await dbContext.Results.AddRangeAsync(results);
        await dbContext.AthleteCourses.AddRangeAsync(athleteCourses);

        await dbContext.SaveChangesAsync();

        var orchestrator = new GetAwardsOrchestrator(dbContext);
        var podiumEntries = await orchestrator.GetPodiumEntries(1);

        var overallPodiumWinners = GetPodiumEntryDto(podiumEntries, "Overall");

        Assert.Equal("Overall", overallPodiumWinners.BracketName);
        Assert.Equal(1, overallPodiumWinners.FirstPlaceAthlete.AthleteId);
        Assert.Equal(1, overallPodiumWinners.FirstPlaceAthlete.AthleteCourseId);
        Assert.Equal("JA1", overallPodiumWinners.FirstPlaceAthlete.FullName);
        Assert.Equal("7:46:40 AM", overallPodiumWinners.FirstPlaceAthlete.FinishTime);
        Assert.Equal("16:40", overallPodiumWinners.FirstPlaceAthlete.PaceWithTime.TimeFormatted);
        Assert.Equal("JA2", overallPodiumWinners.SecondPlaceAthlete.FullName);
        Assert.Equal("JA3", overallPodiumWinners.ThirdPlaceAthlete.FullName);

        var malePodiumWinners = GetPodiumEntryDto(podiumEntries, "Male");

        Assert.Equal("JA5", malePodiumWinners.FirstPlaceAthlete.FullName);
        Assert.Equal("JA6", malePodiumWinners.SecondPlaceAthlete.FullName);
        Assert.Equal("JA7", malePodiumWinners.ThirdPlaceAthlete.FullName);

        var femalePodiumWinners = GetPodiumEntryDto(podiumEntries, "Female");

        Assert.Equal("JA4", femalePodiumWinners.FirstPlaceAthlete.FullName);
        Assert.Equal("JA9", femalePodiumWinners.SecondPlaceAthlete.FullName);
        Assert.Null(femalePodiumWinners.ThirdPlaceAthlete);

        var division8PodiumWinners = GetPodiumEntryDto(podiumEntries, "8 Division");

        Assert.Equal("JA8", division8PodiumWinners.FirstPlaceAthlete.FullName);
        Assert.Equal("JA10", division8PodiumWinners.SecondPlaceAthlete.FullName);
        Assert.Equal("JA12", division8PodiumWinners.ThirdPlaceAthlete.FullName);

        var division13PodiumWinners = GetPodiumEntryDto(podiumEntries, "13 Division");

        Assert.Null(division13PodiumWinners.FirstPlaceAthlete);
        Assert.Null(division13PodiumWinners.SecondPlaceAthlete);
        Assert.Null(division13PodiumWinners.ThirdPlaceAthlete);
    }

    private static PodiumEntryDto GetPodiumEntryDto(List<PodiumEntryDto> podiumEntries, string bracketName)
    {
        return podiumEntries.Single(oo => oo.BracketName == bracketName);
    }
}
