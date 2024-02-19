import { Injectable, OnDestroy, OnInit, inject } from "@angular/core";
import { ActivatedRoute, ParamMap } from "@angular/router";
import { switchMap, of, forkJoin, Subscription } from "rxjs";
import { BreadcrumbRequestDto } from "../../_core/breadcrumbRequestDto";
import { DashboardInfoRequestDto } from "../../_core/dashboardInfoRequestDto";
import { AthletesComponentBase } from "./athletesComponentBase";
import { SearchAthletesRequestDto } from "../../_core/searchAthletesRequestDto";

@Injectable()
export abstract class AthletesByLocationComponentBase extends AthletesComponentBase implements OnInit, OnDestroy {

  private onRouteChangedSubscription: Subscription | null = null
  private route = inject(ActivatedRoute)

  abstract getParamKey(): any
  abstract getDashboardInfoRequestDto(location: string): DashboardInfoRequestDto
  abstract getSearchAthletesRequestDto(location: string): SearchAthletesRequestDto
  abstract getBreadcrumbRequestDto(location: string): BreadcrumbRequestDto

  override ngOnInit(): void {
    super.ngOnInit()

    this.onRouteChangedSubscription = this.route.paramMap.pipe(
      switchMap((paramMap: ParamMap) => {
        const location = paramMap.get(this.getParamKey()) as string
        const dashboardRequest = this.getDashboardInfoRequestDto(location)
        const searchAthletesRequest = this.getSearchAthletesRequestDto(location)
        const breadcrumbRequest = this.getBreadcrumbRequestDto(location)
        
        const observables$ = [
          this.scoringApiService.getDashboardInfo(dashboardRequest),
          this.scoringApiService.getAthletesChunked(searchAthletesRequest),
          this.scoringApiService.getAthletesBreadCrumbsResult(breadcrumbRequest),
          of(location)
        ]

        return forkJoin(observables$)
      })
    ).subscribe(data => {
      this.dashboardInfoResponseDto = data[0]
      this.athleteSearchResultsChunked = data[1]
      this.athletesBreadcrumbResult = data[2]
      this.title = data[3]
      this.showSpinner = false
    })
  }

  override ngOnDestroy() {
    super.ngOnDestroy()
    this.onRouteChangedSubscription!.unsubscribe();
  }
}