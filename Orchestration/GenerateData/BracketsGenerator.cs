using Bogus;
using Core;
using DataModels;
using System.Collections.Generic;

namespace Orchestration.GenerateData
{
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
				var overallBracket = new Bracket(courseId, "Overall", BracketType.Overall);
				yield return overallBracket;
				yield return overallBracket with { Name = "Male", BracketType = BracketType.Gender };
				yield return overallBracket with { Name = "Femail", BracketType = BracketType.Gender };

				var primaryBracketNames = new[] { "M20-25", "F20-25", "M25-30", "F25-30", "M30-35", "F30-35" };
				foreach (var primaryBracketName in primaryBracketNames)
				{
					yield return new Bracket(courseId, primaryBracketName, BracketType.PrimaryDivision);
				}

				foreach (var bracket in nonPrimaryBracketsPool.GetRandomValues(5))
				{
					yield return new Bracket(courseId, bracket.Name, BracketType.NonPrimaryDivision);
				}
			}
		}
	}
}
