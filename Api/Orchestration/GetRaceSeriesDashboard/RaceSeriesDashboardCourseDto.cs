using Api.DataModels;

namespace Api.Orchestration.GetRaceSeriesDashboard;

public record RaceSeriesDashboardCourseDto : DisplayNameWithIdDto
{
    public List<CourseInformationEntryDto> DescriptionEntries { get; }

    public List<CourseInformationEntryDto> PromotionalEntries { get; }

    public List<CourseInformationEntryDto> HowToPrepareEntries { get; }

    public List<RaceSeriesDashboardParticipantDto> Participants { get; }

    public RaceSeriesDashboardCourseDto(int id, string displayName, List<CourseInformationEntry> courseInformationEntries, List<RaceSeriesDashboardParticipantDto> participants) : base(id, displayName)
    {
        DescriptionEntries = GetFilteredParticipants(courseInformationEntries, CourseInformationType.Description);
        PromotionalEntries = GetFilteredParticipants(courseInformationEntries, CourseInformationType.Promotional);
        HowToPrepareEntries = GetFilteredParticipants(courseInformationEntries, CourseInformationType.HowToPrepare);
        Participants = participants;
    }

    private static List<CourseInformationEntryDto> GetFilteredParticipants(List<CourseInformationEntry> courseInformationEntries, CourseInformationType courseInformationType)
    {
        var filteredList = courseInformationEntries.Where(oo => oo.CourseInformationType == courseInformationType);
        return filteredList.Select(oo => new CourseInformationEntryDto { CourseInformationType = oo.CourseInformationType, Description = oo.Description }).ToList();
    }
}
