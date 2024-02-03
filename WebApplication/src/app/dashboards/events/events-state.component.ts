import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { BreadcrumbLocation } from '../../_common/breadcrumbLocation';
import { BreadcrumbRequestDto, BreadcrumbNavigationLevel } from '../../_core/getBreadcrumb/breadcrumbRequestDto';
import { DashboardInfoRequestDto, DashboardInfoType, DashboardInfoLocationType } from '../../_orchestration/getDashboardInfo/dashboardInfoRequestDto';
import { SearchEventsRequestDto } from '../../_orchestration/searchEvents/searchEventsRequestDto';
import { EventsBreadcrumbComponent } from '../../_subComponents/breadcrumbs/events-bread-crumb/events-bread-crumb.component';
import { QuickSearchComponent } from '../quick-search/quick-search.component';
import { SmartNavigationStatesComponent } from '../smart-navigation-states/smart-navigation-states.component';
import { SmartNavigationComponent } from '../smart-navigation/smart-navigation.component';
import { EventSearchResultComponent } from './event-search-result/event-search-result.component';
import { EventsComponentBase } from './eventsComponentBase';
import { HttpClient } from '@angular/common/http';

@Component({
  standalone: true,
  imports: [CommonModule, EventsBreadcrumbComponent, RouterModule, SmartNavigationComponent, SmartNavigationStatesComponent, QuickSearchComponent, EventSearchResultComponent],
  selector: 'app-events-state',
  templateUrl: './events.component.html',
  styleUrls: []
})
export class EventsStateComponent extends EventsComponentBase implements OnInit {

  public isLanding = false

  constructor(route: ActivatedRoute, httpClient: HttpClient) {
    super(route, httpClient)
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
