import { chunk } from 'lodash'
import { ActivatedRoute } from "@angular/router";
import { map } from "rxjs/operators";
import { ApiRequestService } from "../../_services/api-request.service";
import { BreadcrumbComponent } from 'src/app/_common/breadcrumbComponent';
import { SearchAthletesRequestDto } from 'src/app/_orchestration/searchAthletes/searchAthletesRequestDto';
import { AthleteSearchResultDto } from 'src/app/_orchestration/searchAthletes/athleteSearchResultDto';
import { DashboardInfoResponseDto } from 'src/app/_orchestration/getDashboardInfo/dashboardInfoResponseDto';
import { DashboardInfoRequestDto } from 'src/app/_orchestration/getDashboardInfo/dashboardInfoRequestDto';
import { BreadcrumbsApiRequestService } from 'src/app/_services/breadcrumbs-api-request.service';

export abstract class AthletesComponentBase extends BreadcrumbComponent {

  protected static readonly EventsPerRow: number = 4

  public athleteSearchResultsChunked: AthleteSearchResultDto[][]
  public upcomingEvents: any[]
  public athletesUrl: string
  public dashboardInfoResponseDto: DashboardInfoResponseDto
  public title: string

  public athleteIdsToCompare: number[] = []
  public athleteIdsToCompareString: string = null

  constructor(route: ActivatedRoute, apiService: ApiRequestService, breadcrumbApiService: BreadcrumbsApiRequestService) {
    super(route, apiService, breadcrumbApiService)
  }

  protected setDashboardInfo = (requestDto: DashboardInfoRequestDto) => {
    this.apiService.getDashboardInfo(requestDto).subscribe((dashboardInfoResponse: DashboardInfoResponseDto) => {
      this.dashboardInfoResponseDto = dashboardInfoResponse
    })
  }

  protected getAthletes = (searchFilter: SearchAthletesRequestDto) => {

    const getAthletes$ = this.apiService.getAthletes(searchFilter).pipe(
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