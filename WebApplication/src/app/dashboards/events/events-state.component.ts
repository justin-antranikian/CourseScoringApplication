import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap, RouterModule } from '@angular/router';
import { BreadcrumbLocation } from '../../_common/breadcrumbLocation';
import { BreadcrumbRequestDto, BreadcrumbNavigationLevel } from '../../_core/breadcrumbRequestDto';
import { DashboardInfoRequestDto, DashboardInfoType, DashboardInfoLocationType } from '../../_core/dashboardInfoRequestDto';
import { SearchEventsRequestDto } from '../../_core/searchEventsRequestDto';
import { EventsBreadcrumbComponent } from '../../_subComponents/breadcrumbs/events-bread-crumb/events-bread-crumb.component';
import { QuickSearchComponent } from '../quick-search/quick-search.component';
import { SmartNavigationStatesComponent } from '../smart-navigation-states/smart-navigation-states.component';
import { SmartNavigationComponent } from '../smart-navigation/smart-navigation.component';
import { EventSearchResultComponent } from './event-search-result/event-search-result.component';
import { EventsComponentBase } from './eventsComponentBase';
import { HttpClient } from '@angular/common/http';
import { ScoringApiService } from '../../services/scoring-api.service';
import { Subscription, forkJoin, of, switchMap } from 'rxjs';

@Component({
  standalone: true,
  imports: [CommonModule, EventsBreadcrumbComponent, RouterModule, SmartNavigationComponent, SmartNavigationStatesComponent, QuickSearchComponent, EventSearchResultComponent],
  selector: 'app-events-state',
  templateUrl: './events.component.html',
  styleUrls: []
})
export class EventsStateComponent extends EventsComponentBase implements OnInit, OnDestroy {

  private subscription: Subscription | null = null

  constructor(route: ActivatedRoute, httpClient: HttpClient, private scoringApiService: ScoringApiService) {
    super(route, httpClient)
    this.breadcrumbLocation = BreadcrumbLocation.State
  }

  ngOnInit() {
    this.subscription = this.route.paramMap.pipe(
      switchMap((paramMap: ParamMap) => {
        const state = paramMap.get('state') as string
        const dashboardRequest = new DashboardInfoRequestDto(DashboardInfoType.Events, DashboardInfoLocationType.State, state)
        const searchEventsRequest = new SearchEventsRequestDto(null, state)
        const breadcrumbRequest = new BreadcrumbRequestDto(BreadcrumbNavigationLevel.State, state)
        const observables$ = [
          this.scoringApiService.getDashboardInfo(dashboardRequest),
          this.scoringApiService.getRaceSeriesResultsChunked(searchEventsRequest),
          this.scoringApiService.getAthletesBreadCrumbsResult(breadcrumbRequest),
          of(state)
        ]
        return forkJoin(observables$)
      })
    ).subscribe(data => {
      this.dashboardInfoResponseDto = data[0]
      this.eventSearchResultsChunked = data[1]
      this.eventsBreadcrumbResult = data[2]
      this.title = data[3]
    })
  }

  ngOnDestroy() {
    this.subscription?.unsubscribe();
  }
}
