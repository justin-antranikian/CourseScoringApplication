import { Component, OnDestroy, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AthletesComponentBase } from './athletesComponentBase';
import { BreadcrumbLocation } from '../../_common/breadcrumbLocation';
import { DashboardInfoRequestDto, DashboardInfoType, DashboardInfoLocationType } from '../../_core/dashboardInfoRequestDto';
import { SearchAthletesRequestDto } from '../../_core/searchAthletesRequestDto';
import { CommonModule } from '@angular/common';
import { QuickSearchComponent } from '../quick-search/quick-search.component';
import { SmartNavigationComponent } from '../smart-navigation/smart-navigation.component';
import { SmartNavigationStatesComponent } from '../smart-navigation-states/smart-navigation-states.component';
import { LocationInfoRankingsComponent } from '../../_subComponents/location-info-rankings/location-info-rankings.component';
import { BracketRankComponent } from '../../_subComponents/bracket-rank/bracket-rank.component';
import { IntervalTimeComponent } from '../../_subComponents/interval-time/interval-time.component';
import { AthleteBreadcrumbComponent } from '../../_subComponents/breadcrumbs/athlete-bread-crumbs/athlete-bread-crumb.component';
import { NgbToastModule } from '@ng-bootstrap/ng-bootstrap';
import { Subscription, combineLatest } from 'rxjs';

@Component({
  standalone: true,
  selector: 'app-athletes-all',
  templateUrl: './athletes.component.html',
  styleUrl: './athletes.component.css',
  imports: [CommonModule, RouterModule, QuickSearchComponent, SmartNavigationComponent, SmartNavigationStatesComponent, LocationInfoRankingsComponent, BracketRankComponent, IntervalTimeComponent, AthleteBreadcrumbComponent, NgbToastModule, LocationInfoRankingsComponent],
})
export class AthletesAllComponent extends AthletesComponentBase implements OnInit, OnDestroy {

  private getDataSubscription: Subscription | null = null

  constructor() {
    super()
    this.breadcrumbLocation = BreadcrumbLocation.All
    this.isLanding = true
  }

  override ngOnInit() {
    super.ngOnInit()

    this.athletesBreadcrumbResult = { locationInfoWithUrl: null }

    const searchAthletesRequest = new SearchAthletesRequestDto(null, null, null, null)
    const athletes$ = this.scoringApiService.getAthletesChunked(searchAthletesRequest)

    const dashboardRequest = new DashboardInfoRequestDto(DashboardInfoType.Athletes, DashboardInfoLocationType.All)
    const dashboard$ = this.scoringApiService.getDashboardInfo(dashboardRequest)

    this.getDataSubscription = combineLatest([athletes$, dashboard$]).subscribe(data => {
      this.athleteSearchResultsChunked = data[0]
      this.dashboardInfoResponseDto = data[1]
      this.showSpinner = false
    })
  }

  override ngOnDestroy() {
    super.ngOnDestroy()
    this.getDataSubscription!.unsubscribe();
  }
}
