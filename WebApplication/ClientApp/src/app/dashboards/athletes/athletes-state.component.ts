import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiRequestService } from 'src/app/_services/api-request.service';
import { AthletesComponentBase } from './athletesComponentBase';
import { BreadcrumbLocation } from 'src/app/_common/breadcrumbLocation';
import { SearchAthletesRequestDto } from 'src/app/_orchestration/searchAthletes/searchAthletesRequestDto';
import { DashboardInfoLocationType, DashboardInfoRequestDto, DashboardInfoType } from 'src/app/_orchestration/getDashboardInfo/dashboardInfoRequestDto';
import { BreadcrumbRequestDto, BreadcrumbNavigationLevel } from 'src/app/_orchestration/getBreadcrumb/breadcrumbRequestDto';
import { BreadcrumbsApiRequestService } from 'src/app/_services/breadcrumbs-api-request.service';

@Component({
  selector: 'app-athletes-state',
  templateUrl: './athletes.component.html',
  styleUrls: []
})
export class AthletesStateComponent extends AthletesComponentBase implements OnInit {

  constructor(route: ActivatedRoute, apiService: ApiRequestService, breadcrumbApiService: BreadcrumbsApiRequestService) {
    super(route, apiService, breadcrumbApiService)
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
