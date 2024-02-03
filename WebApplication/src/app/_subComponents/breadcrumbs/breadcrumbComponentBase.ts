import { BreadcrumbLocation } from "../../_common/breadcrumbLocation"
import { ComponentBaseWithRoutes } from "../../_common/componentBaseWithRoutes"

export abstract class BreadcrumbComponentBase extends ComponentBaseWithRoutes {
  public breadcrumbAll = BreadcrumbLocation.All
  public breadcrumbState = BreadcrumbLocation.State
  public breadcrumbArea = BreadcrumbLocation.Area
  public breadcrumbCity = BreadcrumbLocation.City
  public breadcrumbRaceSeriesOrArp = BreadcrumbLocation.RaceSeriesOrArp
  public breadcrumbRaceLeaderboard = BreadcrumbLocation.RaceLeaderboard
  public breadcrumbCourseLeaderboard = BreadcrumbLocation.CourseLeaderboard
  public breadcrumbIrp = BreadcrumbLocation.Irp

  /** The last item of the breadcrumb. This needs to be set in the child classes. */
  public title!: string

  // extract as props to render in template.
  public state!: string
  public stateUrl!: string
  public area!: string
  public areaUrl!: string
  public city!: string
  public cityUrl!: string

  /**
   * Sets the state, stateUrl, area, areaUrl, city, and cityUrl properties.
   * @param breadcrumbResultDto 
   */
  protected setLocationInfoWithUrl = (breadcrumbResultDto: any) => {

    if (!breadcrumbResultDto.locationInfoWithUrl) {
      return
    }

    const {
      locationInfoWithUrl: { state, stateUrl, area, areaUrl, city, cityUrl },
    } = breadcrumbResultDto

    this.state = state
    this.stateUrl = stateUrl
    this.area = area
    this.areaUrl = areaUrl
    this.city = city
    this.cityUrl = cityUrl
  }
}