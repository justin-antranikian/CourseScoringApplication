using Api.DataModels;

namespace Api.Orchestration;

public static class BracketsExtensions
{
    public static List<Bracket> FilterBrackets(this IEnumerable<Bracket> brackets, IEnumerable<AthleteCourseBracket> athleteCourseBrackets)
    {
        return brackets.Join(athleteCourseBrackets, oo => oo.Id, oo => oo.BracketId, (bracket, _) => bracket).ToList();
    }
}
