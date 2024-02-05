import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { AthletesComponentBase } from './athletesComponentBase';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { BreadcrumbLocation } from '../../_common/breadcrumbLocation';
import { BreadcrumbRequestDto, BreadcrumbNavigationLevel } from '../../_core/getBreadcrumb/breadcrumbRequestDto';
import { DashboardInfoRequestDto, DashboardInfoType, DashboardInfoLocationType } from '../../_orchestration/getDashboardInfo/dashboardInfoRequestDto';
import { SearchAthletesRequestDto } from '../../_orchestration/searchAthletes/searchAthletesRequestDto';
import { CommonModule } from '@angular/common';
import { QuickSearchComponent } from '../quick-search/quick-search.component';
import { SmartNavigationComponent } from '../smart-navigation/smart-navigation.component';
import { SmartNavigationStatesComponent } from '../smart-navigation-states/smart-navigation-states.component';
import { AthleteSearchResultComponent } from './athlete-search-result/athlete-search-result.component';
import { AthleteBreadcrumbComponent } from '../../_subComponents/breadcrumbs/athlete-bread-crumbs/athlete-bread-crumb.component';
import { NgbToastModule } from '@ng-bootstrap/ng-bootstrap';

@Component({
  standalone: true,
  imports: [CommonModule, HttpClientModule, QuickSearchComponent, SmartNavigationComponent, SmartNavigationStatesComponent, AthleteSearchResultComponent, RouterModule, AthleteBreadcrumbComponent, NgbToastModule],
  selector: 'app-athletes-state',
  templateUrl: './athletes.component.html',
  styleUrls: []
})
export class AthletesStateComponent extends AthletesComponentBase implements OnInit {

  public isLanding = false

  constructor(route: ActivatedRoute, http: HttpClient) {
    super(route, http)
    this.breadcrumbLocation = BreadcrumbLocation.State
    this.athletesUrl = this.EventsStatePage
  }

  ngOnInit() {
    this.route.params.subscribe(() => {
      this.initData()
    });
  }

  private initData = () => {
    this.title = this.getState()

    const breadcrumbRequest = new BreadcrumbRequestDto(BreadcrumbNavigationLevel.State, this.title)
    this.setAthletesBreadcrumbResult(breadcrumbRequest)

    const searchAthletesRequest = new SearchAthletesRequestDto(this.title, null, null, null)
    this.getAthletes(searchAthletesRequest)

    const dashboardRequest = new DashboardInfoRequestDto(DashboardInfoType.Athletes, DashboardInfoLocationType.State, this.title)
    this.setDashboardInfo(dashboardRequest)
  }

  private getState = () => this.route.snapshot.paramMap.get('state')
}
