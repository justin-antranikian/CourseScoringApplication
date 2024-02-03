
namespace Orchestration.GetDashboardInfo;

/// <summary>
/// Events or Athletes
/// </summary>
public enum DashboardInfoType
{
    Events,
    Athletes
}

/// <summary>
/// All, State, Area, or City
/// </summary>
public enum DashboardInfoLocationType
{
    All,
    State,
    Area,
    City
}

public class DashboardInfoRequestDto
{
    public DashboardInfoType DashboardInfoType { get; set; }

    public DashboardInfoLocationType DashboardInfoLocationType { get; set; }

    /// <summary>
    /// colorado, san-diego, greater-miami-area, etc. are examples. Needs to come in with dashes for spaces and lowercase.
    /// </summary>
    public string? LocationTermUrlFriendly { get; set; }
}
