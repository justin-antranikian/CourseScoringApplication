import { chunk } from 'lodash'
import { ActivatedRoute } from "@angular/router";
import { map } from "rxjs/operators";
import { BreadcrumbComponent } from '../../_common/breadcrumbComponent';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { getHttpParams } from '../../_common/httpParamsHelpers';
import { mapRaceSeriesTypeToImageUrl } from '../../_common/IRaceSeriesType';
import { config } from '../../config';


export abstract class EventsComponentBase extends BreadcrumbComponent {

  protected static readonly EventsPerRow: number = 4

  public eventSearchResultsChunked!: any[][];
  public upcomingEvents!: any[];
  public eventsUrl!: string
  public dashboardInfoResponseDto: any
  public title: any

  constructor(route: ActivatedRoute, httpClient: HttpClient) {
    super(route, httpClient)
  }

  public getDashboardInfo(dashboardInfoRequest: any): Observable<any> {
    const httpParams = getHttpParams(dashboardInfoRequest.getAsParamsObject())
    return this.http.get<any>(`${config.apiUrl}/dashboardInfoApi`, httpParams)
  }

  protected setDashboardInfo = (dashboardInfoRequest: any) => {
    this.getDashboardInfo(dashboardInfoRequest).subscribe((dashboardInfoResponse: any) => {
      this.dashboardInfoResponseDto = dashboardInfoResponse
    })
  }

  protected setUpcomingRaces = (searchEventsRequest: any) => {
    this.getRaceSeriesSearchResults(searchEventsRequest).subscribe((results: any) => {
      this.upcomingEvents = results
    })
  }

  public getRaceSeriesSearchResults(searchEventsRequest: any): Observable<any[]> {
    const httpParams = getHttpParams(searchEventsRequest.getAsParamsObject())

    const raceSeriesSearch$ = this.http.get<any[]>(`${config.apiUrl}/raceSeriesSearchApi`, httpParams).pipe(
      map((raceSeriesEntries: any[]): any[] => raceSeriesEntries.map(mapRaceSeriesTypeToImageUrl)),
    )

    return raceSeriesSearch$
  }

  protected getRaceSeriesResults = (searchEventsRequest: any) => {
    const getSeries$ = this.getRaceSeriesSearchResults(searchEventsRequest).pipe(
      map((eventSearchResultDtos: any[]): any[][] => chunk(eventSearchResultDtos, EventsComponentBase.EventsPerRow))
    )

    getSeries$.subscribe((eventSearchResultDtos: any[][]) => { 
      this.eventSearchResultsChunked = eventSearchResultDtos
    })
  }
}