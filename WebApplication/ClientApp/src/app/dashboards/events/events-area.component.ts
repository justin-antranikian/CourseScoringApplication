import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { EventsComponentBase } from 'src/app/dashboards/events/eventsComponentBase';
import { BreadcrumbLocation } from 'src/app/_common/breadcrumbLocation';
import { DashboardInfoLocationType, DashboardInfoRequestDto, DashboardInfoType } from 'src/app/_orchestration/getDashboardInfo/dashboardInfoRequestDto';
import { SearchEventsRequestDto } from 'src/app/_orchestration/searchEvents/searchEventsRequestDto';
import { ApiRequestService } from 'src/app/_services/api-request.service';
import { BreadcrumbRequestDto, BreadcrumbNavigationLevel } from 'src/app/_orchestration/getBreadcrumb/breadcrumbRequestDto';
import { BreadcrumbsApiRequestService } from 'src/app/_services/breadcrumbs-api-request.service';

@Component({
  selector: 'app-events-area',
  templateUrl: './events.component.html',
  styleUrls: []
})
export class EventsAreaComponent extends EventsComponentBase implements OnInit {

  constructor(route: ActivatedRoute, apiService: ApiRequestService, breadcrumbApiService: BreadcrumbsApiRequestService) {
    super(route, apiService, breadcrumbApiService)
    this.breadcrumbLocation = BreadcrumbLocation.Area
    this.eventsUrl = this.AthletesAreaPage
  }

  ngOnInit() {
    this.route.params.subscribe(() => {
      this.initData()
    });
  }

  private initData = () => {
    this.title = this.getArea()

    const searchEventsRequest = new SearchEventsRequestDto(null, null, this.title)
    this.getRaceSeriesResults(searchEventsRequest)

    const searchUpcomingEventsRequest = new SearchEventsRequestDto(null, null, this.title, null, [], true)
    this.setUpcomingRaces(searchUpcomingEventsRequest)

    const breadcrumbRequest = new BreadcrumbRequestDto(BreadcrumbNavigationLevel.Area, this.title)
    this.setEventsBreadcrumbResult(breadcrumbRequest)

    const dashboardRequest = new DashboardInfoRequestDto(DashboardInfoType.Events, DashboardInfoLocationType.Area, this.title)
    this.setDashboardInfo(dashboardRequest)
  }

  private getArea = () => this.route.snapshot.paramMap.get('area')
}
