using Api.DataModels;
using Api.DataModels.Enums;

namespace Api.Orchestration.GetArp;

public static class ArpGoalDtoMapper
{
    public static ArpGoalDto GetArpGoalDto(RaceSeriesType? raceSeriesType, int goalTotal, int actualTotal, double totalDistance, List<Course> courses)
    {
        var raceSeriesTypeName = raceSeriesType?.ToFriendlyText() ?? "All Events";
        var courseDtos = courses.Select(CourseGoalArpDtoMapper.GetCourseGoalArpDto).ToList();
        var percentComplete = GetPercentComplete(actualTotal, goalTotal);

        var arpGoalDto = new ArpGoalDto
        (
            raceSeriesTypeName,
            goalTotal,
            actualTotal,
            totalDistance,
            percentComplete,
            courseDtos
        );

        return arpGoalDto;
    }

    private static double GetPercentComplete(int actualTotal, int goalTotal)
    {
        if (goalTotal == 0)
        {
            return actualTotal == 0 ? 0 : 100;
        }

        var fractionComplete = (double)actualTotal / (double)goalTotal;
        return Math.Round(fractionComplete * 100);
    }
}

public record ArpGoalDto
(
    string RaceSeriesTypeName,
    int GoalTotal,
    int ActualTotal,
    double TotalDistance,
    double PercentComplete,
    List<CourseGoalArpDto> Courses
);
