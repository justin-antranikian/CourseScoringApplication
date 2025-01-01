using Api.DataModels;
using Microsoft.EntityFrameworkCore;

namespace ApiTests.Orchestration;

internal static class ScoringDbContextCreator
{
    internal static ScoringDbContext GetScoringDbContext()
    {
        var dbContext = GetEmptyDbContext();

        dbContext.Courses.AddRange(TestDataGenerator.GetCourses());
        dbContext.Athletes.AddRange(TestDataGenerator.GetAthletes());

        dbContext.SaveChanges();

        var (metadataEntries, results) = TestDataGenerator.GetScoringResults(dbContext, 1, 2, 3);

        dbContext.Results.AddRange(results);
        dbContext.BracketMetadataEntries.AddRange(metadataEntries);

        var athleteCourseBrackets = TestDataGenerator.GetAthleteCourseBrackets();
        dbContext.AtheleteCourseBrackets.AddRange(athleteCourseBrackets);

        dbContext.Races.AddRange(TestDataGenerator.GetRaces());
        dbContext.RaceSeries.AddRange(TestDataGenerator.GetRaceSeries());

        dbContext.SaveChanges();
        return dbContext;
    }

    internal static ScoringDbContext GetEmptyDbContext()
    {
        var options = new DbContextOptionsBuilder<ScoringDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString() + " Test").Options;
        return new ScoringDbContext(options);
    }
}
