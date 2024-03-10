using Core.Enums;

namespace Orchestration.GetRaceSeriesDashboard;

public class CourseInformationEntryDto
{
    public required CourseInformationType CourseInformationType { get; set; }
    public required string Description { get; set; }
}
