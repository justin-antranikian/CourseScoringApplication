using Api.DataModels;
using Core;

namespace Api.Orchestration.GetRaceSeriesDashboard;

public static class RaceSeriesDashboardParticipantDtoMapper
{
    public static RaceSeriesDashboardParticipantDto GetDto(AthleteCourse athleteCourse, Course course)
    {
        var trainingList = athleteCourse.GetTrainingList();
        var athlete = athleteCourse.Athlete;

        return new
        (
            athlete.Id,
            athleteCourse.Id,
            athlete.FullName,
            athlete.FirstName,
            athleteCourse.Bib,
            athlete.State,
            athlete.City,
            DateTimeHelper.GetRaceAge(athlete.DateOfBirth, course.StartDate),
            athlete.Gender.ToAbbreviation(),
            athleteCourse.CourseGoalDescription,
            athleteCourse.PersonalGoalDescription,
            trainingList
        );
    }
}

public record RaceSeriesDashboardParticipantDto
(
    int AthleteId,
    int AthleteCourseId,
    string FullName,
    string FirstName,
    string Bib,
    string State,
    string City,
    int RaceAge,
    string GenderAbbreviated,
    string CourseGoalDescription,
    string PersonalGoalDescription,
    List<string> TrainingList
);
