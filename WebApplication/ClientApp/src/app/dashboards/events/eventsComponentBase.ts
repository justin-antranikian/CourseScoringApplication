import { chunk } from 'lodash'
import { ActivatedRoute } from "@angular/router";
import { map } from "rxjs/operators";
import { EventSearchResultDto } from "../../_orchestration/searchEvents/eventSearchResultDto";
import { SearchEventsRequestDto } from "../../_orchestration/searchEvents/searchEventsRequestDto";
import { ApiRequestService } from "../../_services/api-request.service";
import { BreadcrumbComponent } from 'src/app/_common/breadcrumbComponent';
import { DashboardInfoRequestDto } from 'src/app/_orchestration/getDashboardInfo/dashboardInfoRequestDto';
import { DashboardInfoResponseDto } from 'src/app/_orchestration/getDashboardInfo/dashboardInfoResponseDto';
import { BreadcrumbsApiRequestService } from 'src/app/_services/breadcrumbs-api-request.service';

export abstract class EventsComponentBase extends BreadcrumbComponent {

  protected static readonly EventsPerRow: number = 4

  public eventSearchResultsChunked: EventSearchResultDto[][]
  public upcomingEvents: EventSearchResultDto[]
  public eventsUrl: string
  public dashboardInfoResponseDto: DashboardInfoResponseDto
  public title: string

  constructor(route: ActivatedRoute, apiService: ApiRequestService, breadcrumbApiService: BreadcrumbsApiRequestService) {
    super(route, apiService, breadcrumbApiService)
  }

  protected setDashboardInfo = (dashboardInfoRequest: DashboardInfoRequestDto) => {
    this.apiService.getDashboardInfo(dashboardInfoRequest).subscribe((dashboardInfoResponse: DashboardInfoResponseDto) => {
      this.dashboardInfoResponseDto = dashboardInfoResponse
    })
  }

  protected setUpcomingRaces = (searchEventsRequest: SearchEventsRequestDto) => {
    this.apiService.getRaceSeriesSearchResults(searchEventsRequest).subscribe(results => {
      this.upcomingEvents = results
    })
  }

  protected getRaceSeriesResults = (searchEventsRequest: SearchEventsRequestDto) => {
    const getSeries$ = this.apiService.getRaceSeriesSearchResults(searchEventsRequest).pipe(
      map((eventSearchResultDtos: EventSearchResultDto[]): EventSearchResultDto[][] => chunk(eventSearchResultDtos, EventsComponentBase.EventsPerRow))
    )

    getSeries$.subscribe((eventSearchResultDtos: EventSearchResultDto[][]) => { 
      this.eventSearchResultsChunked = eventSearchResultDtos
    })
  }
}