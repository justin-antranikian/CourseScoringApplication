import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap, RouterModule } from '@angular/router';
import { AthletesComponentBase } from './athletesComponentBase';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { BreadcrumbLocation } from '../../_common/breadcrumbLocation';
import { BreadcrumbRequestDto, BreadcrumbNavigationLevel } from '../../_core/breadcrumbRequestDto';
import { DashboardInfoRequestDto, DashboardInfoType, DashboardInfoLocationType } from '../../_core/dashboardInfoRequestDto';
import { SearchAthletesRequestDto } from '../../_core/searchAthletesRequestDto';
import { CommonModule } from '@angular/common';
import { QuickSearchComponent } from '../quick-search/quick-search.component';
import { SmartNavigationComponent } from '../smart-navigation/smart-navigation.component';
import { SmartNavigationStatesComponent } from '../smart-navigation-states/smart-navigation-states.component';
import { AthleteSearchResultComponent } from './athlete-search-result/athlete-search-result.component';
import { AthleteBreadcrumbComponent } from '../../_subComponents/breadcrumbs/athlete-bread-crumbs/athlete-bread-crumb.component';
import { NgbToastModule } from '@ng-bootstrap/ng-bootstrap';
import { Subject, combineLatest, forkJoin, switchMap, takeUntil, tap } from 'rxjs';

@Component({
  standalone: true,
  imports: [CommonModule, HttpClientModule, QuickSearchComponent, SmartNavigationComponent, SmartNavigationStatesComponent, AthleteSearchResultComponent, RouterModule, AthleteBreadcrumbComponent, NgbToastModule],
  selector: 'app-athletes-state',
  templateUrl: './athletes.component.html',
  styleUrls: []
})
export class AthletesStateComponent extends AthletesComponentBase implements OnInit {

  public isLanding = false
  private onDestroy$ = new Subject<void>();

  constructor(route: ActivatedRoute, http: HttpClient) {
    super(route, http)
    this.breadcrumbLocation = BreadcrumbLocation.State
    this.athletesUrl = this.EventsStatePage
  }

  ngOnInit() {

    this.route.paramMap.pipe(
      takeUntil(this.onDestroy$),
      tap((paramMap: ParamMap) => this.title = paramMap.get('state')),
      switchMap((paramMap: ParamMap) => {
        const state = paramMap.get('state') as string
        const dashboardRequest = new DashboardInfoRequestDto(DashboardInfoType.Athletes, DashboardInfoLocationType.State, state)
        const searchAthletesRequest = new SearchAthletesRequestDto(this.title, null, null, null)
        const breadcrumbRequest = new BreadcrumbRequestDto(BreadcrumbNavigationLevel.State, state)
        const observables$ = [this.getDashboardInfo(dashboardRequest), this.getAthletes2(searchAthletesRequest), this.getAthletesBreadCrumbsResult(breadcrumbRequest)]
        return forkJoin(observables$)
      })
    ).subscribe(data =>
    {
      this.dashboardInfoResponseDto = data[0]
      this.athleteSearchResultsChunked = data[1]
      this.athletesBreadcrumbResult = data[2]
    }
    )
  }

  ngOnDestroy() {
    this.onDestroy$.next();
    this.onDestroy$.complete();
  }
}
