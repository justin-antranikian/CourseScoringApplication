import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { EventsComponentBase } from 'src/app/dashboards/events/eventsComponentBase';
import { BreadcrumbLocation } from 'src/app/_common/breadcrumbLocation';
import { DashboardInfoLocationType, DashboardInfoRequestDto, DashboardInfoType } from 'src/app/_orchestration/getDashboardInfo/dashboardInfoRequestDto';
import { SearchEventsRequestDto } from 'src/app/_orchestration/searchEvents/searchEventsRequestDto';
import { ApiRequestService } from 'src/app/_services/api-request.service';
import { BreadcrumbsApiRequestService } from 'src/app/_services/breadcrumbs-api-request.service';

@Component({
  selector: 'app-events-all',
  templateUrl: './events.component.html',
  styleUrls: []
})
export class EventsAllComponent extends EventsComponentBase implements OnInit {

  public isLanding = true

  constructor(route: ActivatedRoute, apiService: ApiRequestService, breadcrumbApiService: BreadcrumbsApiRequestService ) {
    super(route, apiService, breadcrumbApiService)
    this.breadcrumbLocation = BreadcrumbLocation.All
    this.eventsUrl = this.AthletesPage
  }

  ngOnInit() {
    const searchEventsRequest = new SearchEventsRequestDto(null)
    this.getRaceSeriesResults(searchEventsRequest)

    const searchUpcomingEventsRequest = new SearchEventsRequestDto(null, null, null, null, [], true)
    this.setUpcomingRaces(searchUpcomingEventsRequest)

    this.eventsBreadcrumbResult = {
      locationInfoWithUrl: null,
    }

    const dashboardRequest = new DashboardInfoRequestDto(DashboardInfoType.Events, DashboardInfoLocationType.All)
    this.setDashboardInfo(dashboardRequest)
  }
}
