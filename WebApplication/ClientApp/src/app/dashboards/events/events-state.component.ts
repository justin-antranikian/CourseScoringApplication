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
  selector: 'app-events-state',
  templateUrl: './events.component.html',
  styleUrls: []
})
export class EventsStateComponent extends EventsComponentBase implements OnInit {

  constructor(route: ActivatedRoute, apiService: ApiRequestService, breadcrumbApiService: BreadcrumbsApiRequestService) {
    super(route, apiService, breadcrumbApiService)
    this.breadcrumbLocation = BreadcrumbLocation.State
    this.eventsUrl = this.AthletesStatePage
  }

  ngOnInit() {
    this.route.params.subscribe(() => {
      this.initData()
    });
  }

  private initData = () => {
    this.title = this.getState()

    const searchEventsRequest = new SearchEventsRequestDto(null, this.title)
    this.getRaceSeriesResults(searchEventsRequest)

    const searchUpcomingEventsRequest = new SearchEventsRequestDto(null, this.title, null, null, [], true)
    this.setUpcomingRaces(searchUpcomingEventsRequest)

    const breadcrumbRequest = new BreadcrumbRequestDto(BreadcrumbNavigationLevel.State, this.title)
    this.setEventsBreadcrumbResult(breadcrumbRequest)

    const dashboardRequest = new DashboardInfoRequestDto(DashboardInfoType.Events, DashboardInfoLocationType.State, this.title)
    this.setDashboardInfo(dashboardRequest)
  }

  private getState = () => this.route.snapshot.paramMap.get('state')
}
