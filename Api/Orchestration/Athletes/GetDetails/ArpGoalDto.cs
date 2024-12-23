using Api.DataModels;

namespace Api.Orchestration.Athletes.GetDetails;

public static class ArpGoalDtoMapper
{
    public static ArpGoalDto GetArpGoalDto(RaceSeriesType? raceSeriesType, int goalTotal, int actualTotal, double totalDistance, List<Course> courses)
    {
        var raceSeriesTypeName = raceSeriesType?.ToFriendlyText() ?? "All Events";
        var percentComplete = GetPercentComplete(actualTotal, goalTotal);

        return new ArpGoalDto
        {
            ActualTotal = actualTotal,
            GoalTotal = goalTotal,
            PercentComplete = percentComplete,
            RaceSeriesTypeName = raceSeriesTypeName,
            TotalDistance = totalDistance,
            Courses = courses.Select(CourseGoalArpDtoMapper.GetCourseGoalArpDto).ToList()
        };
    }

    private static double GetPercentComplete(int actualTotal, int goalTotal)
    {
        if (goalTotal == 0)
        {
            return actualTotal == 0 ? 0 : 100;
        }

        var fractionComplete = actualTotal / (double)goalTotal;
        return Math.Round(fractionComplete * 100);
    }
}

public record ArpGoalDto
{
    public required int ActualTotal { get; init; }
    public required int GoalTotal { get; init; }
    public required double PercentComplete { get; init; }
    public required string RaceSeriesTypeName { get; init; }
    public required double TotalDistance { get; init; }
    public required List<CourseGoalArpDto> Courses { get; init; }
}
