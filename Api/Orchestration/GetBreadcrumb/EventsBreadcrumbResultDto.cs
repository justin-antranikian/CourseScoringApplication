namespace Api.Orchestration.GetBreadcrumb;

public record EventsBreadcrumbResultDto(
    LocationInfoWithUrl LocationInfoWithUrl,
    DisplayNameWithIdDto? RaceSeriesDisplayWithId = null,
    DisplayNameWithIdDto? RaceDisplayWithId = null,
    DisplayNameWithIdDto? CourseDisplayWithId = null,
    DisplayNameWithIdDto? IrpDisplayWithId = null)
    : BreadcrumbResultDto(LocationInfoWithUrl);
