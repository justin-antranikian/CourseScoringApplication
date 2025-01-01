using Api.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Api.Orchestration.GenerateData;

public class GenerateDataOrchestrator(ScoringDbContext dbContext)
{
    private async Task GenerateRaceSeriesThroughCourses(List<Location> locations)
    {
        var raceSeries = RaceSeriesGenerator.GetRaceSeries(locations).ToList();
        dbContext.RaceSeries.AddRange(raceSeries);
        await dbContext.SaveChangesAsync();

        var races = RaceGenerator.GetRaces(raceSeries).ToList();
        await dbContext.Races.AddRangeAsync(races);
        await dbContext.SaveChangesAsync();

        var courses = CourseGenerator.GetCourses(races);
        await dbContext.Courses.AddRangeAsync(courses);
        await dbContext.SaveChangesAsync();
    }

    private async Task GenerateBrackets()
    {
        var courses = await dbContext.Courses.ToListAsync();
        var brackets = BracketsGenerator.GetBrackets(courses);

        dbContext.Brackets.AddRange(brackets);
        await dbContext.SaveChangesAsync();
    }

    private async Task GenerateAthletes(List<Location> locations)
    {
        var athletes = AthletesGenerator.GetAthletes(locations);
        await dbContext.Athletes.AddRangeAsync(athletes);
        await dbContext.SaveChangesAsync();

        await dbContext.SaveChangesAsync();
    }

    public async Task GenerateAthleteCoursesAndCourseBrackets()
    {
        var courses = await dbContext.Courses.AsNoTracking().ToListAsync();
        var athletes = await dbContext.Athletes.AsNoTracking().ToListAsync();
        var brackets = await dbContext.Brackets.AsNoTracking().ToListAsync();

        var athleteCourses = AthleteCoursesGenerator.GetAthleteCourses(courses, athletes).ToList();
        await dbContext.AthleteCourses.AddRangeAsync(athleteCourses);
        await dbContext.SaveChangesAsync();

        var athleteCourseBrackets = AthleteCourseBracketGenerator.GetAthleteCourseBrackets(athleteCourses, athletes, brackets).ToList();
        await dbContext.AtheleteCourseBrackets.AddRangeAsync(athleteCourseBrackets);
        await dbContext.SaveChangesAsync();
    }

    public async Task RankAthletes(List<Location> locations)
    {
        var athletes = await dbContext.Athletes.Include(oo => oo.AthleteCourses).ToListAsync();
        AthletesUpdator.RankAthletes(athletes, locations);
        await dbContext.SaveChangesAsync();
    }

    public async Task GenerateTagReads()
    {
        var athleteCourses = await dbContext.AthleteCourses.AsNoTracking().ToListAsync();
        var allIntervals = await dbContext.Intervals.AsNoTracking().ToListAsync();

        var reads = TagReadsGenerator.GetTagReads(allIntervals, athleteCourses);
        await dbContext.TagReads.AddRangeAsync(reads);
        await dbContext.SaveChangesAsync();
    }

    public async Task Generate()
    {
        await GenerateLocations();
        var locations = await dbContext.Locations.ToListAsync();

        await GenerateRaceSeriesThroughCourses(locations);
        await GenerateBrackets();

        await new CreateIntervalsOrchestrator(dbContext).Create();

        await GenerateAthletes(locations);
        await GenerateAthleteCoursesAndCourseBrackets();
        await RankAthletes(locations);

        await GenerateTagReads();
        await new ScoreCoursesOrchestrator(dbContext).Score();
    }

    private async Task GenerateLocations()
    {
        var locations = LocationGenerator.GenerateLocations().ToList();
        await dbContext.Locations.AddRangeAsync(locations);
        await dbContext.SaveChangesAsync();
    }
}
