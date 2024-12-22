using Api.DataModels;
using Bogus;

namespace Api.Orchestration.GenerateData;

public static class BracketsGenerator
{
    public static IEnumerable<Bracket> GetBrackets(List<Course> courses)
    {
        var bracketFaker = new Faker<Bracket>()
            .RuleFor(oo => oo.Name, f => "Team : " + f.Company.CompanyName());

        var nonPrimaryBracketsPool = bracketFaker.Generate(500);

        foreach (var course in courses)
        {
            var courseId = course.Id;

            yield return Bracket.Create(courseId, "Overall", BracketType.Overall);
            yield return Bracket.Create(courseId, "Male", BracketType.Gender);
            yield return Bracket.Create(courseId, "Female", BracketType.Gender);

            var primaryBracketNames = new[] { "M20-25", "F20-25", "M25-30", "F25-30", "M30-35", "F30-35" };
            foreach (var primaryBracketName in primaryBracketNames)
            {
                yield return Bracket.Create(courseId, primaryBracketName, BracketType.PrimaryDivision);
            }

            foreach (var bracket in nonPrimaryBracketsPool.GetRandomValues(5))
            {
                yield return Bracket.Create(courseId, bracket.Name, BracketType.NonPrimaryDivision);
            }
        }
    }
}
