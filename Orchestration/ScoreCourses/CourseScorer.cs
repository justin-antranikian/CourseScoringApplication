namespace Orchestration.ScoreCourses;

public class CourseScorer
{
    private readonly Course _course;
    private readonly List<Bracket> _brackets;
    private readonly List<TagRead> _tagReads;
    private readonly List<AthleteCourseBracket> _atheleteCourseBrackets;
    private readonly List<Interval> _intervals;

    public CourseScorer(Course course, List<Bracket> brackets, List<TagRead> tagReads, List<AthleteCourseBracket> atheleteCourseBrackets, List<Interval> intervals)
    {
        _course = course;
        _brackets = brackets;
        _tagReads = tagReads;
        _atheleteCourseBrackets = atheleteCourseBrackets;
        _intervals = intervals;
    }

    public ScoringResult GetScoringResult()
    {
        var highestIntervalScorer = new HighestIntervalCompletedScorer(_course, _brackets, _tagReads, _atheleteCourseBrackets, _intervals);
        var bracketIntervalScorer = new BracketIntervalScorer(_course, _brackets, _tagReads, _atheleteCourseBrackets, _intervals);

        var highestIntervalResults = highestIntervalScorer.GetResults();
        var (resultsForInterval, intervalMetadata) = bracketIntervalScorer.GetResults();

        var bracketMetadata = _atheleteCourseBrackets.GroupBy(oo => oo.BracketId).Select(oo =>
        {
            return new BracketMetadata(_course.Id, oo.Key, oo.Count());
        });

        var allMetadata = intervalMetadata.Concat(bracketMetadata).ToList();
        var allResults = resultsForInterval.Concat(highestIntervalResults).ToList();
        return new ScoringResult(allMetadata, allResults);
    }
}
