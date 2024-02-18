import { HttpClient } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { EventsComponentBase } from './eventsComponentBase';
import { SearchEventsRequestDto } from '../../_core/searchEventsRequestDto';
import { DashboardInfoLocationType, DashboardInfoRequestDto, DashboardInfoType } from '../../_core/dashboardInfoRequestDto';
import { CommonModule } from '@angular/common';
import { EventsBreadcrumbComponent } from '../../_subComponents/breadcrumbs/events-bread-crumb/events-bread-crumb.component';
import { SmartNavigationComponent } from '../smart-navigation/smart-navigation.component';
import { SmartNavigationStatesComponent } from '../smart-navigation-states/smart-navigation-states.component';
import { QuickSearchComponent } from '../quick-search/quick-search.component';
import { EventSearchResultComponent } from './event-search-result/event-search-result.component';
import { BreadcrumbLocation } from '../../_common/breadcrumbLocation';
import { Subscription, combineLatest } from 'rxjs';
import { ScoringApiService } from '../../services/scoring-api.service';

@Component({
  standalone: true,
  selector: 'app-events-all',
  templateUrl: './events.component.html',
  imports: [CommonModule, EventsBreadcrumbComponent, RouterModule, SmartNavigationComponent, SmartNavigationStatesComponent, QuickSearchComponent, EventSearchResultComponent],
  styleUrls: []
})
export class EventsAllComponent extends EventsComponentBase implements OnInit, OnDestroy {

  private subscription: Subscription | null = null

  constructor(route: ActivatedRoute, httpClient: HttpClient, private scoringApiService: ScoringApiService) {
    super(route, httpClient)
    this.breadcrumbLocation = BreadcrumbLocation.All
    this.isLanding = true
  }

  ngOnInit() {
    this.eventsBreadcrumbResult = {
      locationInfoWithUrl: null,
    }

    const searchEventsRequest = new SearchEventsRequestDto(null)
    const events$ = this.scoringApiService.getRaceSeriesResultsChunked(searchEventsRequest)

    const dashboardRequest = new DashboardInfoRequestDto(DashboardInfoType.Events, DashboardInfoLocationType.All)
    const dashboard$ = this.scoringApiService.getDashboardInfo(dashboardRequest)

    this.subscription = combineLatest([events$, dashboard$]).subscribe(data => {
      this.eventSearchResultsChunked = data[0]
      this.dashboardInfoResponseDto = data[1]
    })
  }

  ngOnDestroy() {
    this.subscription?.unsubscribe();
  }
}
