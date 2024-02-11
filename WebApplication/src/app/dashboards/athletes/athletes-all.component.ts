import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { AthletesComponentBase } from './athletesComponentBase';
import { HttpClient } from '@angular/common/http';
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

@Component({
  standalone: true,
  selector: 'app-athletes-all',
  templateUrl: './athletes.component.html',
  imports: [CommonModule, RouterModule, QuickSearchComponent, SmartNavigationComponent, SmartNavigationStatesComponent, AthleteSearchResultComponent, LocationInfoRankingsComponent, BracketRankComponent, IntervalTimeComponent, AthleteBreadcrumbComponent, NgbToastModule],
  styleUrls: []
})
export class AthletesAllComponent extends AthletesComponentBase implements OnInit {

  public isLanding = true

  constructor(route: ActivatedRoute, http: HttpClient) {
    super(route, http)
    this.breadcrumbLocation = BreadcrumbLocation.All
    this.athletesUrl = this.EventsPage
  }

  ngOnInit() {
    const searchAthletesRequest = new SearchAthletesRequestDto(null, null, null, null)
    this.getAthletes(searchAthletesRequest)

    this.athletesBreadcrumbResult = {
      locationInfoWithUrl: null,
    }

    const dashboardRequest = new DashboardInfoRequestDto(DashboardInfoType.Athletes, DashboardInfoLocationType.All)
    this.setDashboardInfo(dashboardRequest)
  }
}
