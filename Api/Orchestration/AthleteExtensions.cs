using Api.DataModels;

namespace Api.Orchestration;

public static class AthleteExtensions
{
    public static List<string> GetTags(this Athlete athlete)
    {
        return athlete.AthleteRaceSeriesGoals.Select(oo => oo.RaceSeriesType.ToAthleteText()).ToList();
    }

    public static List<string> GetTrainingList(this AthleteCourse athleteCourse)
    {
        return athleteCourse.AthleteCourseTrainings.Select(oo => oo.Description).ToList();
    }
}
