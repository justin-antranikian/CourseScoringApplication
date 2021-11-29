import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { getHttpParams } from 'src/app/_common/httpParamsHelpers';
import { BreadcrumbResultDto } from '../_orchestration/getBreadcrumb/breadcrumbResultDto';
import { EventsBreadcrumbResultDto } from '../_orchestration/getBreadcrumb/eventsBreadcrumbResultDto';
import { BreadcrumbRequestDto } from '../_orchestration/getBreadcrumb/breadcrumbRequestDto';

@Injectable({
  providedIn: 'root'
})
export class BreadcrumbsApiRequestService {

  constructor(
    private readonly http: HttpClient,
    @Inject('BASE_URL') private readonly baseUrl: string
  ) { }

  /**
  * Gets athletes breadcrumb information.
  * @param breadcrumbRequestDto
  */
  public getAthletesBreadCrumbsResult(breadcrumbRequestDto: BreadcrumbRequestDto): Observable<BreadcrumbResultDto> {
    const httpParams = getHttpParams(breadcrumbRequestDto.getAsParamsObject())
    return this.http.get<BreadcrumbResultDto>(this.baseUrl + `athletesBreadCrumbsApi`, httpParams)
  }

  /**
  * Gets events breadcrumb information.
  * @param breadcrumbRequestDto
  */
  public getEventsBreadCrumbsResult(breadcrumbRequestDto: BreadcrumbRequestDto): Observable<EventsBreadcrumbResultDto> {
    const httpParams = getHttpParams(breadcrumbRequestDto.getAsParamsObject())
    return this.http.get<EventsBreadcrumbResultDto>(this.baseUrl + `eventsBreadCrumbsApi`, httpParams)
  }
}
