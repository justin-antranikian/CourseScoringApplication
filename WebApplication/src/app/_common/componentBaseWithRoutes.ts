import { IIntervalType, mapIntervalTypeToImageUrl } from "./IIntervalName"
import { IRaceSeriesType, mapRaceSeriesTypeToImageUrl } from "./IRaceSeriesType"
import { RouteConstants } from "./routeConstants"

export abstract class ComponentBaseWithRoutes {

  protected mapIntervalTypeImages = <T extends IIntervalType>(intervalTypes: T[]): T[] => {
    return intervalTypes.map(mapIntervalTypeToImageUrl)
  }

  protected mapSeriesTypeImages = <T extends IRaceSeriesType>(raceSeriesTypes: T[]): T[] => {
    return raceSeriesTypes.map(mapRaceSeriesTypeToImageUrl)
  }

  public readonly AthletesPage = `/${RouteConstants.AthletesPage}`
  public readonly AthletesStatePage = `/${RouteConstants.AthletesStatePage}`
  public readonly AthletesAreaPage = `/${RouteConstants.AthletesAreaPage}`
  public readonly AthletesCityPage = `/${RouteConstants.AthletesCityPage}`
  public readonly EventsPage = `/${RouteConstants.EventsPage}`
  public readonly EventsStatePage = `/${RouteConstants.EventsStatePage}`
  public readonly EventsAreaPage = `/${RouteConstants.EventsAreaPage}`
  public readonly EventsCityPage = `/${RouteConstants.EventsCityPage}`
  public readonly RaceSeriesDashboardPage = `/${RouteConstants.RaceSeriesDashboardPage}`
  public readonly RaceLeaderboardPage = `/${RouteConstants.RaceLeaderboardPage}`
  public readonly CourseLeaderboardPage = `/${RouteConstants.CourseLeaderboardPage}`
  public readonly IndividualResultPage = `/${RouteConstants.IndividualResultPage}`
  public readonly IndividualResultComparePage = `/${RouteConstants.IndividualResultComparePage}`
  public readonly AthletePage = `/${RouteConstants.AthletePage}`
  public readonly AthleteComparePage = `/${RouteConstants.AthleteComparePage}`
  public readonly AdvancedSearch = `/${RouteConstants.AdvancedSearch}`
  public readonly AwardsPodiumPage = `/${RouteConstants.AwardsPodiumPage}`
  public readonly RacesByMonthReportPage = `/${RouteConstants.RacesByMonthReport}`
  public readonly AthleteResultsReportPage = `/${RouteConstants.AthleteResultsReport}`
}
