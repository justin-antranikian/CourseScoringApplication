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
  selector: 'app-events-area',
  templateUrl: './events.component.html',
  styleUrls: []
})
export class EventsAreaComponent extends EventsComponentBase implements OnInit {

  public isLanding = false

  constructor(route: ActivatedRoute, http: HttpClient) {
    super(route, http)
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
