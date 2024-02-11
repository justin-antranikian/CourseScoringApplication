import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { EventsComponentBase } from './eventsComponentBase';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { BreadcrumbLocation } from '../../_common/breadcrumbLocation';
import { BreadcrumbRequestDto, BreadcrumbNavigationLevel } from '../../_core/breadcrumbRequestDto';
import { DashboardInfoRequestDto, DashboardInfoType, DashboardInfoLocationType } from '../../_core/dashboardInfoRequestDto';
import { SearchEventsRequestDto } from '../../_core/searchEventsRequestDto';
import { EventsBreadcrumbComponent } from '../../_subComponents/breadcrumbs/events-bread-crumb/events-bread-crumb.component';
import { QuickSearchComponent } from '../quick-search/quick-search.component';
import { SmartNavigationStatesComponent } from '../smart-navigation-states/smart-navigation-states.component';
import { SmartNavigationComponent } from '../smart-navigation/smart-navigation.component';
import { EventSearchResultComponent } from './event-search-result/event-search-result.component';

@Component({
  standalone: true,
  imports: [CommonModule, EventsBreadcrumbComponent, RouterModule, SmartNavigationComponent, SmartNavigationStatesComponent, QuickSearchComponent, EventSearchResultComponent],
  selector: 'app-events-city',
  templateUrl: './events.component.html',
  styleUrls: []
})
export class EventsCityComponent extends EventsComponentBase implements OnInit {

  public isLanding = false

  constructor(route: ActivatedRoute, httpClient: HttpClient) {
    super(route, httpClient)
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

  private getCity = () => this.route.snapshot.paramMap.get('city') as any
}
