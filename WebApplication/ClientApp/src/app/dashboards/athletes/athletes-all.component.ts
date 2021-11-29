import { Component, OnInit } from '@angular/core';
import { ApiRequestService } from '../../_services/api-request.service';
import { ActivatedRoute } from '@angular/router';
import { AthletesComponentBase } from './athletesComponentBase';
import { BreadcrumbLocation } from 'src/app/_common/breadcrumbLocation';
import { SearchAthletesRequestDto } from 'src/app/_orchestration/searchAthletes/searchAthletesRequestDto';
import { DashboardInfoLocationType, DashboardInfoRequestDto, DashboardInfoType } from 'src/app/_orchestration/getDashboardInfo/dashboardInfoRequestDto';
import { BreadcrumbsApiRequestService } from 'src/app/_services/breadcrumbs-api-request.service';

@Component({
  selector: 'app-athletes-all',
  templateUrl: './athletes.component.html',
  styleUrls: []
})
export class AthletesAllComponent extends AthletesComponentBase implements OnInit {

  public isLanding = true

  constructor(route: ActivatedRoute, apiService: ApiRequestService, breadcrumbApiService: BreadcrumbsApiRequestService) {
    super(route, apiService, breadcrumbApiService)
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
