using Api.DataModels;
using Api.Orchestration.CreateIntervals;
using Api.Orchestration.ScoreCourses;
using Microsoft.EntityFrameworkCore;

namespace Api.Orchestration.GenerateData;

public class GenerateDataOrchestrator(ScoringDbContext scoringDbContext)
{
    private async Task GenerateRaceSeriesThroughCourses()
    {
        var raceSeries = RaceSeriesGenerator.GetRaceSeries();
        scoringDbContext.RaceSeries.AddRange(raceSeries);
        await scoringDbContext.SaveChangesAsync();

        var races = RaceGenerator.GetRaces(raceSeries).ToList();
        await scoringDbContext.Races.AddRangeAsync(races);
        await scoringDbContext.SaveChangesAsync();

        var courses = CourseGenerator.GetCourses(races);
        await scoringDbContext.Courses.AddRangeAsync(courses);
        await scoringDbContext.SaveChangesAsync();
    }

    private async Task GenerateBrackets()
    {
        var courses = await scoringDbContext.Courses.ToListAsync();
        var brackets = BracketsGenerator.GetBrackets(courses);

        scoringDbContext.Brackets.AddRange(brackets);
        await scoringDbContext.SaveChangesAsync();
    }

    private async Task GenerateAthletes()
    {
        var athletes = AthletesGenerator.GetAthletes();
        await scoringDbContext.Athletes.AddRangeAsync(athletes);
        await scoringDbContext.SaveChangesAsync();

        await scoringDbContext.SaveChangesAsync();
    }

    public async Task GenerateAthleteCoursesAndCourseBrackets()
    {
        var courses = await scoringDbContext.Courses.AsNoTracking().ToListAsync();
        var athletes = await scoringDbContext.Athletes.AsNoTracking().ToListAsync();
        var brackets = await scoringDbContext.Brackets.AsNoTracking().ToListAsync();

        var athleteCourses = AthleteCoursesGenerator.GetAthleteCourses(courses, athletes).ToList();
        await scoringDbContext.AthleteCourses.AddRangeAsync(athleteCourses);
        await scoringDbContext.SaveChangesAsync();

        var athleteCourseBrackets = AthleteCourseBracketGenerator.GetAthleteCourseBrackets(athleteCourses, athletes, brackets).ToList();
        await scoringDbContext.AtheleteCourseBrackets.AddRangeAsync(athleteCourseBrackets);
        await scoringDbContext.SaveChangesAsync();
    }

    public async Task RankAthletes()
    {
        var athletes = await scoringDbContext.Athletes.Include(oo => oo.AthleteCourses).ToListAsync();
        AthletesUpdator.RankAthletes(athletes);
        await scoringDbContext.SaveChangesAsync();
    }

    public async Task GenerateTagReads()
    {
        var athleteCourses = await scoringDbContext.AthleteCourses.AsNoTracking().ToListAsync();
        var allIntervals = await scoringDbContext.Intervals.AsNoTracking().ToListAsync();

        var reads = TagReadsGenerator.GetTagReads(allIntervals, athleteCourses);
        await scoringDbContext.TagReads.AddRangeAsync(reads);
        await scoringDbContext.SaveChangesAsync();
    }

    public async Task Generate()
    {
        await GenerateRaceSeriesThroughCourses();
        await GenerateBrackets();

        await new CreateIntervalsOrchestrator(scoringDbContext).Create();

        await GenerateAthletes();
        await GenerateAthleteCoursesAndCourseBrackets();
        await RankAthletes();

        await GenerateTagReads();
        await new ScoreCoursesOrchestrator(scoringDbContext).Score();
    }
}
