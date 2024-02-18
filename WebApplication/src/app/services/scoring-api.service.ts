import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { IRaceSeriesType, mapRaceSeriesTypeToImageUrl } from '../_common/IRaceSeriesType';
import { config } from '../config';
import { HttpClient } from '@angular/common/http';
import { IIntervalType, mapIntervalTypeToImageUrl } from '../_common/IIntervalName';
import { getHttpParams } from '../_common/httpParamsHelpers';
import { SearchIrpsRequestDto } from '../_subComponents/irp-search/SearchIrpsRequestDto';
import { IrpSearchResultDto } from '../_subComponents/irp-search/IrpSearchResultDto';

@Injectable({
  providedIn: 'root'
})
export class ScoringApiService {

  constructor(private readonly http: HttpClient) { }

  protected mapIntervalTypeImages = <T extends IIntervalType>(intervalTypes: T[]): T[] => {
    return intervalTypes.map(mapIntervalTypeToImageUrl)
  }

  protected mapSeriesTypeImages = <T extends IRaceSeriesType>(raceSeriesTypes: T[]): T[] => {
    return raceSeriesTypes.map(mapRaceSeriesTypeToImageUrl)
  }

  public getEventsBreadCrumbsResult(breadcrumbRequestDto: any): Observable<any> {
    const httpParams = getHttpParams(breadcrumbRequestDto.getAsParamsObject())
    return this.http.get<any>(`${config.apiUrl}/eventsBreadCrumbsApi`, httpParams)
  }

  private getBaseObservableForGetRequest (route: string) {
    return this.http.get<any>(`${config.apiUrl}/${route}`)
  }

  public getIrpsFromSearch(irpSearchRequest: SearchIrpsRequestDto): Observable<IrpSearchResultDto[]> {
    const httpParams = getHttpParams(irpSearchRequest.getAsParamsObject())
    return this.http.get<IrpSearchResultDto[]>(`${config.apiUrl}/searchIrpsApi`, httpParams)
  }

  public getCourseInfo(courseId: number): Observable<any> {
    return this.getBaseObservableForGetRequest(`getCourseInfoApi/${courseId}`).pipe(
      map((irpCompareResult: any): any => mapRaceSeriesTypeToImageUrl(irpCompareResult)),
    )
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
      const httpParams = getHttpParams(courseLeaderboardFilter.getAsParams())
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
}
