import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IrpDto } from '../_orchestration/getIrp/irpDto';
import { ArpDto } from '../_orchestration/getArp/arpDto';
import { CourseLeaderboardDto } from '../_orchestration/getLeaderboard/getCourseLeaderboard/courseLeaderboardDto';
import { RaceLeaderboardDto } from '../_orchestration/getLeaderboard/getRaceLeaderboard/raceLeaderboardDto';
import { RaceSeriesDashboardDto } from '../_orchestration/getRaceSeriesDashboard/raceSeriesDashboardDto';
import { SearchEventsRequestDto } from '../_orchestration/searchEvents/searchEventsRequestDto';
import { EventSearchResultDto } from '../_orchestration/searchEvents/eventSearchResultDto';
import { getHttpParams } from '../_common/httpParamsHelpers';
import { map } from 'rxjs/operators';
import { IRaceSeriesType, mapRaceSeriesTypeToImageUrl } from '../_common/IRaceSeriesType';
import { IIntervalType, mapIntervalTypeToImageUrl } from '../_common/IIntervalName';
import { CourseLeaderboardFilter } from '../leaderboard/course-leaderboard/courseLeaderboardFilter';
import { SearchIrpsRequestDto } from '../_orchestration/searchIrps/searchIrpsRequestDto';
import { SearchAthletesRequestDto } from '../_orchestration/searchAthletes/searchAthletesRequestDto';
import { AthleteSearchResultDto } from '../_orchestration/searchAthletes/athleteSearchResultDto';
import { IrpSearchResultDto } from '../_orchestration/searchIrps/irpSearchResultDto';
import { DashboardInfoRequestDto } from '../_orchestration/getDashboardInfo/dashboardInfoRequestDto';
import { DashboardInfoResponseDto } from '../_orchestration/getDashboardInfo/dashboardInfoResponseDto';
import { GetCompetetorsForIrpDto } from '../_orchestration/getCompetetorsForIrp/GetCompetetorsForIrpDto';

@Injectable({
  providedIn: 'root'
})
export class ApiRequestService {

  constructor(
    private readonly http: HttpClient,
    @Inject('BASE_URL') private readonly baseUrl: string
  ) { }

  private mapSeriesTypeImages = <T extends IRaceSeriesType>(raceSeriesTypes: T[]): T[] => {
    return raceSeriesTypes.map(mapRaceSeriesTypeToImageUrl)
  }

  private mapIntervalTypeImages = <T extends IIntervalType>(intervalTypes: T[]): T[] => {
    return intervalTypes.map(mapIntervalTypeToImageUrl)
  }

  /**
  * Gets athletes based on the search criteria.
  */
  public getAthletes(searchAthletesRequest: SearchAthletesRequestDto): Observable<AthleteSearchResultDto[]> {
    const httpParams = getHttpParams(searchAthletesRequest.getAsParamsObject())
    return this.http.get<AthleteSearchResultDto[]>(this.baseUrl + `athleteSearchApi`, httpParams)
  }

  public getAthletesToCompare(athleteIds: number[]): Observable<any> {
    const body: any = {
      AthleteIds: athleteIds
    };

    const url = this.baseUrl + `compareAthletesApi`
    return this.http.post<any>(url, body)
  }

  /**
  * Searches for athletes for a given course.
  */
  public searchIrps(irpSearchRequest: SearchIrpsRequestDto): Observable<IrpSearchResultDto[]> {
    const httpParams = getHttpParams(irpSearchRequest.getAsParamsObject())
    return this.http.get<IrpSearchResultDto[]>(this.baseUrl + `searchIrpsApi`, httpParams)
  }

  /**
   * Gets the dashboard info for both athletes and events.
   * @param dashboardInfoRequest
   */
  public getDashboardInfo(dashboardInfoRequest: DashboardInfoRequestDto): Observable<DashboardInfoResponseDto> {
    const httpParams = getHttpParams(dashboardInfoRequest.getAsParamsObject())
    return this.http.get<DashboardInfoResponseDto>(this.baseUrl + `dashboardInfoApi`, httpParams)
  }

  /**
  * Searches the raceSeriesEntries.
  * @param searchEventsRequest
  */
  public getRaceSeriesSearchResults(searchEventsRequest: SearchEventsRequestDto): Observable<EventSearchResultDto[]> {
    const httpParams = getHttpParams(searchEventsRequest.getAsParamsObject())

    const raceSeriesSearch$ = this.http.get<EventSearchResultDto[]>(this.baseUrl + `raceSeriesSearchApi`, httpParams).pipe(
      map((raceSeriesEntries: EventSearchResultDto[]): EventSearchResultDto[] => raceSeriesEntries.map(mapRaceSeriesTypeToImageUrl)),
    )

    return raceSeriesSearch$
  }

  /**
  * Gets dashboard for a raceSeries.
  * @param raceSeriesId
  */
  public getRaceSeriesDashboardDto(raceSeriesId: number): Observable<RaceSeriesDashboardDto> {

    const getRaceSeriesDashboard$ = this.http.get<RaceSeriesDashboardDto>(this.baseUrl + `raceSeriesDashboardApi/${raceSeriesId}`).pipe(
      map((raceSeriesDashboardDto: RaceSeriesDashboardDto): RaceSeriesDashboardDto => ({
        ...mapRaceSeriesTypeToImageUrl(raceSeriesDashboardDto),
      }))
    )

    return getRaceSeriesDashboard$
  }

  /**
  * Gets raceLeaderboard which is the top 3 athletes of each course.
  * @param raceId
  */
  public getRaceLeaderboard(raceId: number): Observable<RaceLeaderboardDto> {

    const getRaceLeaderboard$ = this.http.get<RaceLeaderboardDto>(this.baseUrl + `raceLeaderboardApi/${raceId}`).pipe(
      map((raceLeaderboardDto: RaceLeaderboardDto): RaceLeaderboardDto => ({
        ...mapRaceSeriesTypeToImageUrl(raceLeaderboardDto),
        leaderboards: this.mapIntervalTypeImages(raceLeaderboardDto.leaderboards)
      }))
    )

    return getRaceLeaderboard$
  }

  /**
  * Gets the courseLeaderboard which is the top athletes for an interval.
  * @param courseId
  * @param courseLeaderboardFilter
  */
  public getCourseLeaderboard(courseId: number, courseLeaderboardFilter: CourseLeaderboardFilter = null): Observable<CourseLeaderboardDto> {

    const courseLeaderboard$ = this.getCourseLeaderboard$(courseId, courseLeaderboardFilter).pipe(
      map((courseLeaderboardDto: CourseLeaderboardDto): CourseLeaderboardDto => ({
        ...mapRaceSeriesTypeToImageUrl(courseLeaderboardDto),
        leaderboards: this.mapIntervalTypeImages(courseLeaderboardDto.leaderboards)
      }))
    )

    return courseLeaderboard$
  }

  private getCourseLeaderboard$(courseId: number, courseLeaderboardFilter: CourseLeaderboardFilter): Observable<CourseLeaderboardDto> {
    const url = this.baseUrl + `courseLeaderboardApi/${courseId}`

    if (courseLeaderboardFilter) {
      const httpParams = getHttpParams(courseLeaderboardFilter.getAsParams())
      return this.http.get<CourseLeaderboardDto>(url, httpParams)
    }

    return this.http.get<CourseLeaderboardDto>(url)
  }

  /**
  * Gets Irp (Individual Results Page) dto.
  * @param athleteCourseId
  */
  public getIrpDto(athleteCourseId: number): Observable<IrpDto> {

    const getIrpDto$ = this.http.get<IrpDto>(this.baseUrl + `irpApi/${athleteCourseId}`).pipe(
      map(mapRaceSeriesTypeToImageUrl),
      map((irpDto: IrpDto): IrpDto => ({
        ...irpDto,
        intervalResults: this.mapIntervalTypeImages(irpDto.intervalResults)
      }))
    )

    return getIrpDto$
  }

  /**
  * Gets Irp (Individual Results Page) dto.
  * @param athleteCourseId
  */
  public getIrpCompetetors(athleteCourseId: number): Observable<GetCompetetorsForIrpDto> {
    return this.http.get<GetCompetetorsForIrpDto>(this.baseUrl + `irpCompetetorsApi/${athleteCourseId}`)
  }

  public getRacesByMonthReport(): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl + `racesByMonthReportApi`).pipe(
      map((byMonthDtos: any): any => {
        return byMonthDtos.map(byMonthDto => ({
          monthName: byMonthDto.monthName,
          races: byMonthDto.races.map(oo => mapRaceSeriesTypeToImageUrl(oo)),
        }))
      })
    )
  }

  public getAthleteResultsReport(): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl + `athleteResultsReportApi`).pipe(
      map((athleteReportResults: any): any => {
        return athleteReportResults.map(athleteReportResult => ({
          ...athleteReportResult,
          athleteReportCourseDtos: athleteReportResult.athleteReportCourseDtos.map(oo => mapRaceSeriesTypeToImageUrl(oo)),
        }))
      })
    )
  }

  /**
  * Gets Irps to compare.
  * @param athleteCourseId
  */
  public getIrpToCompare(athleteCourseIds: number[]): Observable<any> {
    const body: any = {
      AthleteCourseIds: athleteCourseIds
    };

    return this.http.post<any>(this.baseUrl + `compareIrpApi`, body)
  }

  /**
  * Gets Arp (Athlete Results Page) dto.
  * @param athleteId
  */
  public getArpDto(athleteId: number): Observable<ArpDto> {
    return this.http.get<ArpDto>(this.baseUrl + `arpApi/${athleteId}`).pipe(
      map((arpDto: ArpDto): ArpDto => ({
        ...arpDto,
        results: this.mapSeriesTypeImages(arpDto.results)
      }))
    )
  }

  /**
  * Searches both events and athletes on a given search term.
  * @param searchTerm
  */
  public searchAllEntities(searchTerm: string): Observable<any> {

    const properties = {
      searchTerm,
    }

    const httpParams = getHttpParams(properties)
    return this.http.get<any>(this.baseUrl + `searchAllEntitiesSearchApi`, httpParams)
  }

  /**
  * Gets awards podium for a course.
  * @param courseId
  */
  public getAwardsPodium(courseId: number): Observable<any> {
    return this.http.get<any>(this.baseUrl + `awardsPodiumApi/${courseId}`)
  }

  /**
  * Gets awards podium for a course.
  * @param courseId
  */
  public getCourseInfo(courseId: number): Observable<any> {
    return this.http.get<any>(this.baseUrl + `getCourseInfoApi/${courseId}`).pipe(
      map((irpCompareResult: any): any => mapRaceSeriesTypeToImageUrl(irpCompareResult)),
    )
  }

  /**
  * Gets awards podium for a course.
  * @param courseId
  */
  public getCourseStatistics(courseId: number): Observable<any> {
    return this.http.get<any>(this.baseUrl + `courseStatisticsApi/${courseId}`)
  }
}
