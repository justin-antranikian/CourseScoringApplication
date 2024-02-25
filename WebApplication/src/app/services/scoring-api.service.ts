import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { IRaceSeriesType, mapRaceSeriesTypeToImageUrl } from '../_common/IRaceSeriesType';
import { config } from '../config';
import { HttpClient, HttpParams } from '@angular/common/http';
import { IIntervalType, mapIntervalTypeToImageUrl } from '../_common/IIntervalName';
import { SearchIrpsRequestDto } from '../leaderboard/irp-search/SearchIrpsRequestDto';
import { IrpSearchResultDto } from '../leaderboard/irp-search/IrpSearchResultDto';
import { DashboardInfoRequestDto } from '../_core/dashboardInfoRequestDto';
import { DashboardInfoResponseDto } from '../_core/dashboardInfoResponseDto';
import { chunk } from 'lodash';
import { AthleteSearchResultDto } from '../dashboards/athletes/athleteSearchResultDto';
import { SearchAthletesRequestDto } from '../_core/searchAthletesRequestDto';
import { EventSearchResultDto } from '../dashboards/events/eventSearchResultDto';

@Injectable({
  providedIn: 'root'
})
export class ScoringApiService {

  constructor(private http: HttpClient) { }

  private getHttpParams = (fromObject: any) => {
    const httpParams = new HttpParams({ fromObject })
    return { params: httpParams }
  }

  private getBaseObservableForGetRequest (route: string) {
    return this.http.get<any>(`${config.apiUrl}/${route}`)
  }

  protected mapIntervalTypeImages = <T extends IIntervalType>(intervalTypes: T[]): T[] => {
    return intervalTypes.map(mapIntervalTypeToImageUrl)
  }

  protected mapSeriesTypeImages = <T extends IRaceSeriesType>(raceSeriesTypes: T[]): T[] => {
    return raceSeriesTypes.map(mapRaceSeriesTypeToImageUrl)
  }

  public getEventsBreadCrumbsResult(breadcrumbRequestDto: any): Observable<any> {
    const httpParams = this.getHttpParams(breadcrumbRequestDto.getAsParamsObject())
    return this.http.get<any>(`${config.apiUrl}/eventsBreadCrumbsApi`, httpParams)
  }

  public getAthletesBreadCrumbsResult(breadcrumbRequestDto: any): Observable<any> {
    const httpParams = this.getHttpParams(breadcrumbRequestDto.getAsParamsObject())
    return this.http.get<any>(`${config.apiUrl}/athletesBreadCrumbsApi`, httpParams)
  }

  public getAthletes(searchAthletesRequest: SearchAthletesRequestDto): Observable<AthleteSearchResultDto[]> {
    const httpParams = this.getHttpParams(searchAthletesRequest.getAsParamsObject())
    return this.http.get<AthleteSearchResultDto[]>(`${config.apiUrl}/athleteSearchApi`, httpParams)
  }

  public getAthletesChunked = (searchFilter: SearchAthletesRequestDto) => {
    return this.getAthletes(searchFilter).pipe(
      map((athletes: AthleteSearchResultDto[]): AthleteSearchResultDto[][] => chunk(athletes, 4))
    )
  }

  public getRaceSeriesResults(searchEventsRequest: any): Observable<EventSearchResultDto[]> {
    const httpParams = this.getHttpParams(searchEventsRequest.getAsParamsObject())

    const raceSeriesSearch$ = this.http.get<EventSearchResultDto[]>(`${config.apiUrl}/raceSeriesSearchApi`, httpParams).pipe(
      map((raceSeriesEntries: EventSearchResultDto[]): EventSearchResultDto[] => raceSeriesEntries.map(mapRaceSeriesTypeToImageUrl)),
    )

    return raceSeriesSearch$
  }

  public getRaceSeriesResultsChunked = (searchEventsRequest: any) => {
    return this.getRaceSeriesResults(searchEventsRequest).pipe(
      map((eventSearchResultDtos: EventSearchResultDto[]): EventSearchResultDto[][] => chunk(eventSearchResultDtos, 4))
    )
  }

  public getRaceSeriesDashboardDto = (raceSeriesId: number): Observable<any> => {

    const getRaceSeriesDashboard$ = this.http.get<any>(`${config.apiUrl}/raceSeriesDashboardApi/${raceSeriesId}`).pipe(
      map((raceSeriesDashboardDto: any): any => ({
        ...mapRaceSeriesTypeToImageUrl(raceSeriesDashboardDto),
      }))
    )

    return getRaceSeriesDashboard$
  }

  public getDashboardInfo(dashboardInfoRequest: DashboardInfoRequestDto): Observable<DashboardInfoResponseDto> {
    const httpParams = this.getHttpParams(dashboardInfoRequest.getAsParamsObject())
    return this.http.get<DashboardInfoResponseDto>(`${config.apiUrl}/dashboardInfoApi`, httpParams)
  }

  public getIrpsFromSearch(irpSearchRequest: SearchIrpsRequestDto): Observable<IrpSearchResultDto[]> {
    const httpParams = this.getHttpParams(irpSearchRequest.getAsParamsObject())
    return this.http.get<IrpSearchResultDto[]>(`${config.apiUrl}/searchIrpsApi`, httpParams)
  }

  public getRaceLeaderboard(raceId: number): Observable<any> {
    return this.getBaseObservableForGetRequest(`raceLeaderboardApi/${raceId}`).pipe(
      map((raceLeaderboardDto: any): any => ({
        ...mapRaceSeriesTypeToImageUrl(raceLeaderboardDto),
        leaderboards: this.mapIntervalTypeImages(raceLeaderboardDto.leaderboards)
      }))
    )
  }

  private getCourseLeaderboard$(courseId: number, courseLeaderboardFilter: any): Observable<any> {
    const url = `${config.apiUrl}/courseLeaderboardApi/${courseId}`

    if (courseLeaderboardFilter) {
      const httpParams = this.getHttpParams(courseLeaderboardFilter.getAsParams())
      return this.http.get<any>(url, httpParams)
    }

    return this.http.get<any>(url)
  }

  public getCourseLeaderboard(courseId: number, courseLeaderboardFilter: any = null): Observable<any> {
    return this.getCourseLeaderboard$(courseId, courseLeaderboardFilter).pipe(
      map((courseLeaderboardDto: any): any => ({
        ...mapRaceSeriesTypeToImageUrl(courseLeaderboardDto),
        leaderboards: this.mapIntervalTypeImages(courseLeaderboardDto.leaderboards)
      }))
    )
  }

  public getAwardsPodium(courseId: number): Observable<any> {
    return this.getBaseObservableForGetRequest(`awardsPodiumApi/${courseId}`)
  }

  public getIrpDto(athleteCourseId: number): Observable<any> {
    return this.getBaseObservableForGetRequest(`irpApi/${athleteCourseId}`).pipe(
      map((irpDto: any): any => ({
        ...irpDto,
        intervalResults: this.mapIntervalTypeImages(irpDto.intervalResults)
      }))
    )
  }

  public getIrpToCompare(athleteCourseIds: number[]): Observable<any> {
    const body: any = {
      AthleteCourseIds: athleteCourseIds
    };

    return this.http.post<any>(`${config.apiUrl}/compareIrpApi`, body)
  }

  public getArpDto(athleteId: number): Observable<any> {
    return this.getBaseObservableForGetRequest(`arpApi/${athleteId}`).pipe(
      map((arpDto: any): any => ({
        ...arpDto,
        results: this.mapSeriesTypeImages(arpDto.results)
      }))
    )
  }

  public getAthletesToCompare(athleteIds: number[]): Observable<any> {
    const body: any = {
      AthleteIds: athleteIds
    };

    return this.http.post<any>(`${config.apiUrl}/compareAthletesApi`, body)
  }

  public searchAllEntities = (searchTerm: string) => {
    const url = `${config.apiUrl}/SearchAllEntitiesSearchApi?searchTerm=${searchTerm}`
    const httpParams = this.getHttpParams(searchTerm)
    return this.http.get<any>(url, httpParams)
  }
}
