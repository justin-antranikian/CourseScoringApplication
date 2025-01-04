using Api.DataModels;

namespace Api.Orchestration.GenerateData;

public static class AthleteCoursesGenerator
{
    public static IEnumerable<AthleteCourse> GetAthleteCourses(List<Course> courses, List<Athlete> athletes)
    {
        var random = new Random();

        var courseGoals = new[]
        {
            "My goal for the course is to place in the top 10 percent. I am training to be a top 5 percent finisher in my gender and age bracket.",
            "My goal for the course is to place in the top 75 percent.",
            "I want to beat all my rivals.",
            "I want to win my age bracket.",
            "I want to win my gender bracket.",
            "I want to win the overall bracket.",
            "I don't really have any course goals yet. Just want to finish.",
        };

        var personalGoals = new[]
        {
            "Last year I ran a 8:30 pace. This year I am aiming for a 8:20 pace.",
            "This is my first time doing this event so I don't have a personal goal yet.",
            "Last year I didn't finish. This year I would like to finish without taking too many breaks.",
            "Would like to finish this race so I can complete my third event for the year.",
        };

        var tranings = new[]
        {
            "Running 3 times a week",
            "Mountain biking a few times a week",
            "Swimming every week or 2.",
            "Lifting weights 3-4 times a week",
            "Hiking a couple times a week",
            "Low fat, high protein diet",
            "Staying away from candy and junk food",
            "Road biking a few times a week",
            "Rest days when hips are too sore",
        };

        foreach (var course in courses)
        {
            var randomTake = random.Next(1, 10);
            var athletePool = athletes.GetRandomValues(randomTake);

            var index = 1;
            foreach (var athlete in athletePool)
            {
                var athleteCourseTrainings = new[]
                {
                    tranings.GetRandomValue(),
                    tranings.GetRandomValue(),
                    tranings.GetRandomValue(),
                    tranings.GetRandomValue(),
                };

                var athleteCourse = new AthleteCourse
                {
                    AthleteId = athlete.Id,
                    Bib = "A" + index,
                    CourseId = course.Id,
                    CourseGoalDescription = courseGoals.GetRandomValue(),
                    PersonalGoalDescription = personalGoals.GetRandomValue(),
                    AthleteCourseTrainings = athleteCourseTrainings.Distinct().Select(oo => new AthleteCourseTraining { Description = oo }).ToList()
                };

                yield return athleteCourse;
                index++;
            }
        }
    }
}
