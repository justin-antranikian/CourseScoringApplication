import { chunk } from 'lodash'
import { ActivatedRoute } from "@angular/router";
import { map } from "rxjs/operators";
import { BreadcrumbComponent } from '../../_common/breadcrumbComponent';
import { HttpClient } from '@angular/common/http';
import { DashboardInfoRequestDto } from '../../_orchestration/getDashboardInfo/dashboardInfoRequestDto';
import { DashboardInfoResponseDto } from '../../_orchestration/getDashboardInfo/dashboardInfoResponseDto';
import { AthleteSearchResultDto } from '../../_orchestration/searchAthletes/athleteSearchResultDto';
import { SearchAthletesRequestDto } from '../../_orchestration/searchAthletes/searchAthletesRequestDto';
import { getHttpParams } from '../../_common/httpParamsHelpers';
import { Observable } from 'rxjs';
// import { ApiRequestService } from "../../_services/api-request.service";
// import { BreadcrumbComponent } from 'src/app/_common/breadcrumbComponent';
// import { SearchAthletesRequestDto } from 'src/app/_orchestration/searchAthletes/searchAthletesRequestDto';
// import { AthleteSearchResultDto } from 'src/app/_orchestration/searchAthletes/athleteSearchResultDto';
// import { DashboardInfoResponseDto } from 'src/app/_orchestration/getDashboardInfo/dashboardInfoResponseDto';
// import { DashboardInfoRequestDto } from 'src/app/_orchestration/getDashboardInfo/dashboardInfoRequestDto';
// import { BreadcrumbsApiRequestService } from 'src/app/_services/breadcrumbs-api-request.service';

export abstract class AthletesComponentBase extends BreadcrumbComponent {

  public getAthletesHttp(searchAthletesRequest: SearchAthletesRequestDto): Observable<AthleteSearchResultDto[]> {
    const httpParams = getHttpParams(searchAthletesRequest.getAsParamsObject())
    return this.http.get<AthleteSearchResultDto[]>(`https://localhost:44308/athleteSearchApi`, httpParams)
  }

  public getDashboardInfo(dashboardInfoRequest: DashboardInfoRequestDto): Observable<DashboardInfoResponseDto> {
    const httpParams = getHttpParams(dashboardInfoRequest.getAsParamsObject())
    return this.http.get<DashboardInfoResponseDto>(`https://localhost:44308/dashboardInfoApi`, httpParams)
  }

  protected static readonly EventsPerRow: number = 4

  public athleteSearchResultsChunked!: any[][]
  public upcomingEvents!: any[]
  public athletesUrl!: string
  public dashboardInfoResponseDto: any
  public title: any

  public athleteIdsToCompare: number[] = []
  public athleteIdsToCompareString: any = null

  constructor(route: ActivatedRoute, http: HttpClient) {
    super(route, http)
  }

  protected setDashboardInfo = (requestDto: DashboardInfoRequestDto) => {
    this.getDashboardInfo(requestDto).subscribe((dashboardInfoResponse: DashboardInfoResponseDto) => {
      this.dashboardInfoResponseDto = dashboardInfoResponse
    })
  }

  protected getAthletes = (searchFilter: SearchAthletesRequestDto) => {

    const getAthletes$ = this.getAthletesHttp(searchFilter).pipe(
      map((athletes: AthleteSearchResultDto[]): AthleteSearchResultDto[][] => chunk(athletes, AthletesComponentBase.EventsPerRow))
    )

    getAthletes$.subscribe((athleteSearchDtos: AthleteSearchResultDto[][]) => { 
      this.athleteSearchResultsChunked = athleteSearchDtos
    })
  }

  public onCompareClicked = ({ id }: AthleteSearchResultDto) => {
    const athleteIds = this.athleteIdsToCompare

    if (!athleteIds.includes(id)) {
      athleteIds.push(id)
    } else {
      const index = athleteIds.indexOf(id);
      athleteIds.splice(index, 1);
    }

    this.athleteIdsToCompareString = JSON.stringify(athleteIds)
  }
}