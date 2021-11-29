
namespace Orchestration.GetRaceSeriesDashboard
{
	public record PastRaceDto(int Id, string DisplayName, string KickOffDate) : DisplayNameWithIdDto(Id, DisplayName);
}
