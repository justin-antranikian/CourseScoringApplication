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
import { AthleteSearchResultComponent } from './athlete-search-result/athlete-search-result.component';
import { LocationInfoRankingsComponent } from '../../_subComponents/location-info-rankings/location-info-rankings.component';
import { BracketRankComponent } from '../../_subComponents/bracket-rank/bracket-rank.component';
import { IntervalTimeComponent } from '../../_subComponents/interval-time/interval-time.component';
import { AthleteBreadcrumbComponent } from '../../_subComponents/breadcrumbs/athlete-bread-crumbs/athlete-bread-crumb.component';
import { NgbToastModule } from '@ng-bootstrap/ng-bootstrap';
import { ScoringApiService } from '../../services/scoring-api.service';
import { Subscription, combineLatest } from 'rxjs';

@Component({
  standalone: true,
  selector: 'app-athletes-all',
  templateUrl: './athletes.component.html',
  imports: [CommonModule, RouterModule, QuickSearchComponent, SmartNavigationComponent, SmartNavigationStatesComponent, AthleteSearchResultComponent, LocationInfoRankingsComponent, BracketRankComponent, IntervalTimeComponent, AthleteBreadcrumbComponent, NgbToastModule],
  styleUrls: []
})
export class AthletesAllComponent extends AthletesComponentBase implements OnInit, OnDestroy {

  private subscription: Subscription | null = null

  constructor(private scoringApiService: ScoringApiService) {
    super()
    this.breadcrumbLocation = BreadcrumbLocation.All
    this.isLanding = true
  }

  ngOnInit() {
    this.athletesBreadcrumbResult = {
      locationInfoWithUrl: null,
    }

    const searchAthletesRequest = new SearchAthletesRequestDto(null, null, null, null)
    const athletes$ = this.scoringApiService.getAthletesChunked(searchAthletesRequest)

    const dashboardRequest = new DashboardInfoRequestDto(DashboardInfoType.Athletes, DashboardInfoLocationType.All)
    const dashboard$ = this.scoringApiService.getDashboardInfo(dashboardRequest)

    this.subscription = combineLatest([athletes$, dashboard$]).subscribe(data => {
      this.athleteSearchResultsChunked = data[0]
      this.dashboardInfoResponseDto = data[1]
    })
  }

  ngOnDestroy() {
    this.subscription?.unsubscribe();
  }
}
