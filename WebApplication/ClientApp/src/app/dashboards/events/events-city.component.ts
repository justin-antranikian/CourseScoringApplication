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
  selector: 'app-events-city',
  templateUrl: './events.component.html',
  styleUrls: []
})
export class EventsCityComponent extends EventsComponentBase implements OnInit {

  constructor(route: ActivatedRoute, apiService: ApiRequestService, breadcrumbApiService: BreadcrumbsApiRequestService) {
    super(route, apiService, breadcrumbApiService)
    this.breadcrumbLocation = BreadcrumbLocation.City
    this.eventsUrl = this.AthletesCityPage
  }

  ngOnInit() {
    this.route.params.subscribe(() => {
      this.initData()
    });
  }

  private initData = () => {
    this.title = this.getCity()

    const searchEventsRequest = new SearchEventsRequestDto(null, null, null, this.title)
    this.getRaceSeriesResults(searchEventsRequest)

    const searchUpcomingEventsRequest = new SearchEventsRequestDto(null, null, null, this.title, [], true)
    this.setUpcomingRaces(searchUpcomingEventsRequest)

    const breadcrumbRequest = new BreadcrumbRequestDto(BreadcrumbNavigationLevel.City, this.title)
    this.setEventsBreadcrumbResult(breadcrumbRequest)

    const dashboardInfoRequest = new DashboardInfoRequestDto(DashboardInfoType.Events, DashboardInfoLocationType.City, this.title)
    this.setDashboardInfo(dashboardInfoRequest)
  }

  private getCity = () => this.route.snapshot.paramMap.get('city')
}
