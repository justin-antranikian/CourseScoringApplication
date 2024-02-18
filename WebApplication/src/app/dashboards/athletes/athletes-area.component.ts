import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { AthletesComponentBase } from './athletesComponentBase';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { BreadcrumbLocation } from '../../_common/breadcrumbLocation';
import { BreadcrumbRequestDto, BreadcrumbNavigationLevel } from '../../_core/breadcrumbRequestDto';
import { DashboardInfoRequestDto, DashboardInfoType, DashboardInfoLocationType } from '../../_core/dashboardInfoRequestDto';
import { SearchAthletesRequestDto } from '../../_core/searchAthletesRequestDto';
import { CommonModule } from '@angular/common';
import { QuickSearchComponent } from '../quick-search/quick-search.component';
import { SmartNavigationStatesComponent } from '../smart-navigation-states/smart-navigation-states.component';
import { SmartNavigationComponent } from '../smart-navigation/smart-navigation.component';
import { AthleteSearchResultComponent } from './athlete-search-result/athlete-search-result.component';
import { AthleteBreadcrumbComponent } from '../../_subComponents/breadcrumbs/athlete-bread-crumbs/athlete-bread-crumb.component';
import { NgbToastModule } from '@ng-bootstrap/ng-bootstrap';

@Component({
  standalone: true,
  imports: [CommonModule, HttpClientModule, QuickSearchComponent, SmartNavigationComponent, SmartNavigationStatesComponent, AthleteSearchResultComponent, RouterModule, AthleteBreadcrumbComponent, NgbToastModule],
  selector: 'app-athletes-area',
  templateUrl: './athletes.component.html',
  styleUrls: []
})
export class AthletesAreaComponent extends AthletesComponentBase implements OnInit {

  public isLanding = false

  constructor(route: ActivatedRoute, http: HttpClient) {
    super(route, http)
    this.breadcrumbLocation = BreadcrumbLocation.Area
    this.athletesUrl = this.EventsAreaPage
  }

  ngOnInit() {
    this.route.params.subscribe(() => {
      this.initData()
    });
  }

  private initData = () => {
    this.title = this.getArea()

    const breadcrumbRequest = new BreadcrumbRequestDto(BreadcrumbNavigationLevel.Area, this.title)
    this.setAthletesBreadcrumbResult(breadcrumbRequest)

    const searchAthletesRequest = new SearchAthletesRequestDto(null, this.title, null, null)
    this.getAthletes(searchAthletesRequest)

    const dashboardRequest = new DashboardInfoRequestDto(DashboardInfoType.Athletes, DashboardInfoLocationType.Area, this.title)
    this.dashboardInfoResponseDto$ = this.getDashboardInfo(dashboardRequest)

    // this.setDashboardInfo(dashboardRequest)
  }

  private getArea = () => this.route.snapshot.paramMap.get('area')
}
