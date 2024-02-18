import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { IRaceSeriesType, mapRaceSeriesTypeToImageUrl } from '../_common/IRaceSeriesType';
import { config } from '../config';
import { HttpClient } from '@angular/common/http';
import { IIntervalType, mapIntervalTypeToImageUrl } from '../_common/IIntervalName';
import { getHttpParams } from '../_common/httpParamsHelpers';

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

  public getIrpDto(athleteCourseId: number): Observable<any> {
    return this.getBaseObservableForGetRequest(`irpApi/${athleteCourseId}`).pipe(
      map((irpDto: any): any => ({
        ...irpDto,
        intervalResults: this.mapIntervalTypeImages(irpDto.intervalResults)
      }))
    )
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
