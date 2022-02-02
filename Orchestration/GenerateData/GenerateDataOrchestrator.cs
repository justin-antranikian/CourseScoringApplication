using Microsoft.EntityFrameworkCore;
using Orchestration.CreateIntervals;
using Orchestration.ScoreCourses;
using System.Threading.Tasks;

namespace Orchestration.GenerateData;

public class GenerateDataOrchestrator
{
	private readonly ScoringDbContext _scoringDbContext;

	public GenerateDataOrchestrator(ScoringDbContext scoringDbContext)
	{
		_scoringDbContext = scoringDbContext;
	}

	private async Task GenerateRaceSeriesThroughCourses()
	{
		var raceSeries = RaceSeriesGenerator.GetRaceSeries();
		_scoringDbContext.RaceSeries.AddRange(raceSeries);
		await _scoringDbContext.SaveChangesAsync();

		var races = RaceGenerator.GetRaces(raceSeries).ToList();
		await _scoringDbContext.Races.AddRangeAsync(races);
		await _scoringDbContext.SaveChangesAsync();

		var courses = CourseGenerator.GetCourses(races);
		await _scoringDbContext.Courses.AddRangeAsync(courses);
		await _scoringDbContext.SaveChangesAsync();
	}

	private async Task GenerateBrackets()
	{
		var courses = await _scoringDbContext.Courses.ToListAsync();
		var brackets = BracketsGenerator.GetBrackets(courses);

		_scoringDbContext.Brackets.AddRange(brackets);
		await _scoringDbContext.SaveChangesAsync();
	}

	private async Task GenerateAthletes()
	{
		var athletes = AthletesGenerator.GetAthletes();
		await _scoringDbContext.Athletes.AddRangeAsync(athletes);
		await _scoringDbContext.SaveChangesAsync();

		var persistedAthletes = await _scoringDbContext.Athletes.ToListAsync();
		AthletesUpdator.SetRelationships(persistedAthletes);
		await _scoringDbContext.SaveChangesAsync();
	}

	public async Task GenerateAthleteCoursesAndCourseBrackets()
	{
		var courses = await _scoringDbContext.Courses.ToListAsync();
		var athletes = await _scoringDbContext.Athletes.ToListAsync();
		var brackets = await _scoringDbContext.Brackets.ToListAsync();

		var athleteCourses = AthleteCoursesGenerator.GetAthleteCourses(courses, athletes).ToList();
		await _scoringDbContext.AthleteCourses.AddRangeAsync(athleteCourses);
		await _scoringDbContext.SaveChangesAsync();

		var athleteCourseBrackets = AthleteCourseBracketGenerator.GetAthleteCourseBrackets(athleteCourses, athletes, brackets);
		await _scoringDbContext.AtheleteCourseBrackets.AddRangeAsync(athleteCourseBrackets);
		await _scoringDbContext.SaveChangesAsync();
	}

	public async Task RankAthletes()
	{
		var athletes = await _scoringDbContext.Athletes.Include(oo => oo.AthleteCourses).ToListAsync();
		AthletesUpdator.RankAthletes(athletes);
		await _scoringDbContext.SaveChangesAsync();
	}

	public async Task GenerateTagReads()
	{
		var athleteCourses = await _scoringDbContext.AthleteCourses.ToListAsync();
		var allIntervals = await _scoringDbContext.Intervals.ToListAsync();

		var reads = TagReadsGenerator.GetTagReads(allIntervals, athleteCourses);
		await _scoringDbContext.TagReads.AddRangeAsync(reads);
		await _scoringDbContext.SaveChangesAsync();
	}

	public async Task Generate()
	{
        await GenerateRaceSeriesThroughCourses();
        await GenerateBrackets();

        await new CreateIntervalsOrchestrator(_scoringDbContext).Create();

        await GenerateAthletes();
        await GenerateAthleteCoursesAndCourseBrackets();
        await RankAthletes();

        await GenerateTagReads();
        await new ScoreCoursesOrchestrator(_scoringDbContext).Score();
	}
}
