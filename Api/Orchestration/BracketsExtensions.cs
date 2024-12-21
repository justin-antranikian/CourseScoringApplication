using Api.DataModels;

namespace Api.Orchestration;

public static class BracketsExtensions
{
    public static List<Bracket> FilterBrackets(this IEnumerable<Bracket> brackets, IEnumerable<BracketMetadata> metadataEntries)
    {
        var filter = brackets.Join(metadataEntries,
                        bracket => bracket.Id,
                        meta => meta.BracketId,
                        (bracket, _) => bracket);

        return filter.ToList();
    }

    public static List<Bracket> FilterBrackets(this IEnumerable<Bracket> brackets, IEnumerable<AthleteCourseBracket> athleteCourseBrackets)
    {
        var filter = brackets.Join(athleteCourseBrackets,
                        bracket => bracket.Id,
                        athleteBracket => athleteBracket.BracketId,
                        (bracket, _) => bracket);

        return filter.ToList();
    }
}
