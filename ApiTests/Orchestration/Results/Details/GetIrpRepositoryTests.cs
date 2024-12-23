﻿using Api.Orchestration.Results.GetDetails;

namespace ApiTests.Orchestration.Results.Details;

public class GetIrpRepositoryTests
{
    [Fact]
    public async Task GetQueryResult_ReturnsCorrectResult()
    {
        var dbContext = ScoringDbContextCreator.GetScoringDbContext();

        var repo = new GetIrpRepository(dbContext);
        var (results, course, metadataEntries, athleteCourse) = await repo.GetQueryResult(1);

        Assert.Equal(1, course.Id);
        Assert.Equal(7, course.Brackets.Count);
        Assert.Equal(6, course.Intervals.Count);

        Assert.Equal(4, metadataEntries.Count);

        Assert.Equal(1, athleteCourse.Id);
        Assert.Equal(4, athleteCourse.AthleteCourseBrackets.Count);

        Assert.Equal(6, results.Count);
    }
}
