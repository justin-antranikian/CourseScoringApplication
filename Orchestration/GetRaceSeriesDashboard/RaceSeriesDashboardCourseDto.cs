using Core.Enums;

namespace Orchestration.GetRaceSeriesDashboard;

public record RaceSeriesDashboardCourseDto : DisplayNameWithIdDto
{
	public List<CourseInformationEntry> DescriptionEntries { get; }

	public List<CourseInformationEntry> PromotionalEntries { get; }

	public List<CourseInformationEntry> HowToPrepareEntries { get; }

	public List<RaceSeriesDashboardParticipantDto> Participants { get; }

	public RaceSeriesDashboardCourseDto(int id, string displayName, List<CourseInformationEntry> courseInformationEntries, List<RaceSeriesDashboardParticipantDto> participants) : base(id, displayName)
	{
		DescriptionEntries = GetFilteredParticipants(courseInformationEntries, CourseInformationType.Description);
		PromotionalEntries = GetFilteredParticipants(courseInformationEntries, CourseInformationType.Promotional);
		HowToPrepareEntries = GetFilteredParticipants(courseInformationEntries, CourseInformationType.HowToPrepare);
		Participants = participants;
	}

	private static List<CourseInformationEntry> GetFilteredParticipants(List<CourseInformationEntry> courseInformationEntries, CourseInformationType courseInformationType)
	{
		return courseInformationEntries.Where(oo => oo.CourseInformationType == courseInformationType).ToList();
	}
}
