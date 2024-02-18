import { ActivatedRoute } from "@angular/router";
import { ComponentBaseWithRoutes } from "./componentBaseWithRoutes";
import { Observable, map } from "rxjs";
import { getHttpParams } from './httpParamsHelpers';
import { HttpClient } from "@angular/common/http";
import { mapRaceSeriesTypeToImageUrl } from "./IRaceSeriesType";
import { config } from "../config";

export abstract class BreadcrumbComponent extends ComponentBaseWithRoutes {

  public breadcrumbLocation: any
  public eventsBreadcrumbResult: any
  public athletesBreadcrumbResult: any

  constructor(
    protected readonly route: ActivatedRoute,
    protected readonly http: HttpClient
  ) { super() }

  public getCourseInfo(courseId: number): Observable<any> {
    return this.http.get<any>(`${config.apiUrl}/getCourseInfoApi/${courseId}`).pipe(
      map((irpCompareResult: any): any => mapRaceSeriesTypeToImageUrl(irpCompareResult)),
    )
  }

  public getRaceSeriesDashboardDto(raceSeriesId: number): Observable<any> {

    const getRaceSeriesDashboard$ = this.http.get<any>(`${config.apiUrl}/raceSeriesDashboardApi/${raceSeriesId}`).pipe(
      map((raceSeriesDashboardDto: any): any => ({
        ...mapRaceSeriesTypeToImageUrl(raceSeriesDashboardDto),
      }))
    )

    return getRaceSeriesDashboard$
  }

  public getRaceLeaderboard(raceId: number): Observable<any> {

    const getRaceLeaderboard$ = this.http.get<any>(`${config.apiUrl}/raceLeaderboardApi/${raceId}`).pipe(
      map((raceLeaderboardDto: any): any => ({
        ...mapRaceSeriesTypeToImageUrl(raceLeaderboardDto),
        leaderboards: this.mapIntervalTypeImages(raceLeaderboardDto.leaderboards)
      }))
    )

    return getRaceLeaderboard$
  }

  /**
  * Gets athletes breadcrumb information.
  * @param breadcrumbRequestDto
  */
  public getAthletesBreadCrumbsResult(breadcrumbRequestDto: any): Observable<any> {
    const httpParams = getHttpParams(breadcrumbRequestDto.getAsParamsObject())
    return this.http.get<any>(`${config.apiUrl}/athletesBreadCrumbsApi`, httpParams)
  }

  /**
  * Gets events breadcrumb information.
  * @param breadcrumbRequestDto
  */
  public getEventsBreadCrumbsResult(breadcrumbRequestDto: any): Observable<any> {
    const httpParams = getHttpParams(breadcrumbRequestDto.getAsParamsObject())
    return this.http.get<any>(`${config.apiUrl}/eventsBreadCrumbsApi`, httpParams)
  }

  protected getId = () => parseInt(this.route.snapshot.paramMap.get('id') as any)

  /**
   * calls api and sets athletesBreadcrumbResult property.
   * @param breadcrumbRequestDto 
   */
  protected setAthletesBreadcrumbResult = (breadcrumbRequestDto: any) => {

    const getBreadcrumbs$ = this.getAthletesBreadCrumbsResult(breadcrumbRequestDto)

    getBreadcrumbs$.subscribe((breadcrumbResult: any) => {
      this.athletesBreadcrumbResult = breadcrumbResult
    })
  }

  /**
  * calls api and sets eventsBreadcrumbResult property.
  * @param BreadcrumbRequestDto
  */
  protected setEventsBreadcrumbResult = (breadcrumbRequestDto: any) => {

    const getBreadcrumbs$ = this.getEventsBreadCrumbsResult(breadcrumbRequestDto)

    getBreadcrumbs$.subscribe((breadcrumbResultDto: any) => {
      this.eventsBreadcrumbResult = breadcrumbResultDto
    })
  }
}