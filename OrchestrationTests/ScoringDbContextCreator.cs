using DataModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace OrchestrationTests;

internal static class ScoringDbContextCreator
{
	internal static ScoringDbContext GetScoringDbContext()
	{
		var scoringDbContext = GetEmptyDbContext();

		scoringDbContext.Courses.AddRange(TestDataGenerator.GetCourses());
		scoringDbContext.Athletes.AddRange(TestDataGenerator.GetAthletes());

		scoringDbContext.SaveChanges();

		var (metadataEntries, results) = TestDataGenerator.GetScoringResults(scoringDbContext, 1, 2, 3);

		scoringDbContext.Results.AddRange(results);
		scoringDbContext.BracketMetadataEntries.AddRange(metadataEntries);

		var athleteCourseBrackets = TestDataGenerator.GetAthleteCourseBrackets();
		scoringDbContext.AtheleteCourseBrackets.AddRange(athleteCourseBrackets);

		scoringDbContext.Races.AddRange(TestDataGenerator.GetRaces());
		scoringDbContext.RaceSeries.AddRange(TestDataGenerator.GetRaceSeries());

		scoringDbContext.SaveChanges();
		return scoringDbContext;
	}

	internal static ScoringDbContext GetEmptyDbContext()
	{
		var options = new DbContextOptionsBuilder<ScoringDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString() + " Test").Options;
		return new ScoringDbContext(options);
	}
}
