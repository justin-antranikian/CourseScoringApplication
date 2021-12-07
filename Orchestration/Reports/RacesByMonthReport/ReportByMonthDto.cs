namespace Orchestration.Reports.RacesByMonthReport;

public record ReportByMonthRaceDto(int Id, string Name, DateTime KickOffDate, RaceSeriesType RaceSeriesType);

public record ReportByMonthDto(string MonthName, List<ReportByMonthRaceDto> Races);
