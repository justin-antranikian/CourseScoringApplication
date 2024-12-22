using Api.DataModels;

namespace Api.Orchestration.GenerateData;

public class CourseScorer
(
    Course course,
    List<Bracket> brackets,
    List<TagRead> tagReads,
    List<AthleteCourseBracket> atheleteCourseBrackets,
    List<Interval> intervals)
{
    public ScoringResult GetScoringResult()
    {
        var highestIntervalScorer = new HighestIntervalCompletedScorer(course, brackets, tagReads, atheleteCourseBrackets, intervals);
        var bracketIntervalScorer = new BracketIntervalScorer(course, brackets, tagReads, atheleteCourseBrackets, intervals);

        var highestIntervalResults = highestIntervalScorer.GetResults();
        var (resultsForInterval, intervalMetadata) = bracketIntervalScorer.GetResults();
        var bracketMetadata = atheleteCourseBrackets.GroupBy(oo => oo.BracketId).Select(oo => BracketMetadata.Create(oo.Key, course.Id, null, oo.Count()));

        var allMetadata = intervalMetadata.Concat(bracketMetadata).ToList();
        var allResults = resultsForInterval.Concat(highestIntervalResults).ToList();
        return new ScoringResult(allMetadata, allResults);
    }
}
