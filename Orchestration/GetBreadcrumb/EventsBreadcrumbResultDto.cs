namespace Orchestration.GetBreadcrumb;

public record EventsBreadcrumbResultDto : BreadcrumbResultDto
{
    public DisplayNameWithIdDto? RaceSeriesDisplayWithId { get; }

    public DisplayNameWithIdDto? RaceDisplayWithId { get; }

    public DisplayNameWithIdDto? CourseDisplayWithId { get; }

    public DisplayNameWithIdDto? IrpDisplayWithId { get; }

    public EventsBreadcrumbResultDto
    (
        LocationInfoWithUrl locationInfoWithUrl,
        DisplayNameWithIdDto? raceSeriesDisplayWithId = null,
        DisplayNameWithIdDto? raceDisplayWithId = null,
        DisplayNameWithIdDto? courseDisplayWithId = null,
        DisplayNameWithIdDto? irpDisplayWithId = null
    ) : base(locationInfoWithUrl)
    {
        RaceSeriesDisplayWithId = raceSeriesDisplayWithId;
        RaceDisplayWithId = raceDisplayWithId;
        CourseDisplayWithId = courseDisplayWithId;
        IrpDisplayWithId = irpDisplayWithId;
    }
}
