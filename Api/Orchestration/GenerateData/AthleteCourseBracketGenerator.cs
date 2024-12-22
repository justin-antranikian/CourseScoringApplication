using Api.DataModels;

namespace Api.Orchestration.GenerateData;

public static class AthleteCourseBracketGenerator
{
    public static IEnumerable<AthleteCourseBracket> GetAthleteCourseBrackets(List<AthleteCourse> athleteCourses, List<Athlete> athletes, List<Bracket> brackets)
    {
        foreach (var athleteCourse in athleteCourses)
        {
            var athlete = athletes.Single(oo => oo.Id == athleteCourse.AthleteId);
            var bracketsForCourse = brackets.Where(oo => oo.CourseId == athleteCourse.CourseId).ToList();
            var overallBracket = bracketsForCourse.Single(oo => oo.BracketType == BracketType.Overall);

            var genderBracketName = athlete.Gender == Gender.Male ? "Male" : "Female";
            var genderBracket = bracketsForCourse.Single(oo => oo.Name == genderBracketName);
            var primaryBracket = bracketsForCourse.Where(oo => oo.BracketType == BracketType.PrimaryDivision).GetRandomValue();
            var nonPrimaryBracket = bracketsForCourse.Where(oo => oo.BracketType == BracketType.NonPrimaryDivision).GetRandomValue();

            yield return AthleteCourseBracket.Create(athleteCourse.Id, overallBracket.Id, athleteCourse.CourseId);
            yield return AthleteCourseBracket.Create(athleteCourse.Id, genderBracket.Id, athleteCourse.CourseId);
            yield return AthleteCourseBracket.Create(athleteCourse.Id, primaryBracket.Id, athleteCourse.CourseId);
            yield return AthleteCourseBracket.Create(athleteCourse.Id, nonPrimaryBracket.Id, athleteCourse.CourseId);
        }
    }
}
