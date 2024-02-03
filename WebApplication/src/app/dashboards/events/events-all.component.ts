import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { EventsComponentBase } from './eventsComponentBase';
import { SearchEventsRequestDto } from '../../_orchestration/searchEvents/searchEventsRequestDto';
import { DashboardInfoLocationType, DashboardInfoRequestDto, DashboardInfoType } from '../../_orchestration/getDashboardInfo/dashboardInfoRequestDto';
import { CommonModule } from '@angular/common';
import { EventsBreadcrumbComponent } from '../../_subComponents/breadcrumbs/events-bread-crumb/events-bread-crumb.component';
import { SmartNavigationComponent } from '../smart-navigation/smart-navigation.component';
import { SmartNavigationStatesComponent } from '../smart-navigation-states/smart-navigation-states.component';
import { QuickSearchComponent } from '../quick-search/quick-search.component';
import { EventSearchResultComponent } from './event-search-result/event-search-result.component';
import { BreadcrumbLocation } from '../../_common/breadcrumbLocation';
// import { EventsComponentBase } from 'src/app/dashboards/events/eventsComponentBase';
// import { BreadcrumbLocation } from 'src/app/_common/breadcrumbLocation';
// import { DashboardInfoLocationType, DashboardInfoRequestDto, DashboardInfoType } from 'src/app/_orchestration/getDashboardInfo/dashboardInfoRequestDto';
// import { SearchEventsRequestDto } from 'src/app/_orchestration/searchEvents/searchEventsRequestDto';
// import { ApiRequestService } from 'src/app/_services/api-request.service';
// import { BreadcrumbsApiRequestService } from 'src/app/_services/breadcrumbs-api-request.service';

@Component({
  standalone: true,
  selector: 'app-events-all',
  templateUrl: './events.component.html',
  imports: [CommonModule, EventsBreadcrumbComponent, RouterModule, SmartNavigationComponent, SmartNavigationStatesComponent, QuickSearchComponent, EventSearchResultComponent],
  styleUrls: []
})
export class EventsAllComponent extends EventsComponentBase implements OnInit {

  public isLanding = true

  constructor(route: ActivatedRoute, httpClient: HttpClient) {
    super(route, httpClient)
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
