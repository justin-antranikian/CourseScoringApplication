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
  selector: 'app-athletes-area',
  templateUrl: './athletes.component.html',
  styleUrls: []
})
export class AthletesAreaComponent extends AthletesComponentBase implements OnInit {

  constructor(route: ActivatedRoute, apiService: ApiRequestService, breadcrumbApiService: BreadcrumbsApiRequestService) {
    super(route, apiService, breadcrumbApiService)
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
    this.setDashboardInfo(dashboardRequest)
  }

  private getArea = () => this.route.snapshot.paramMap.get('area')
}
