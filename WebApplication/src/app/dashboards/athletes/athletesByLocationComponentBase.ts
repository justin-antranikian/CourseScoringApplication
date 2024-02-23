import { Injectable, OnDestroy, OnInit, inject } from "@angular/core";
import { ActivatedRoute, ParamMap } from "@angular/router";
import { switchMap, of, forkJoin, Subscription, tap } from "rxjs";
import { BreadcrumbNavigationLevel, BreadcrumbRequestDto } from "../../_core/breadcrumbRequestDto";
import { DashboardInfoLocationType, DashboardInfoRequestDto, DashboardInfoType } from "../../_core/dashboardInfoRequestDto";
import { AthletesComponentBase } from "./athletesComponentBase";
import { SearchAthletesRequestDto } from "../../_core/searchAthletesRequestDto";

@Injectable()
export abstract class AthletesByLocationComponentBase extends AthletesComponentBase implements OnInit, OnDestroy {

  private onRouteChangedSubscription: Subscription | null = null
  private route = inject(ActivatedRoute)

  protected abstract getParamKey(): any
  protected abstract getSearchAthletesRequestDto(location: string): SearchAthletesRequestDto
  protected abstract getDashboardInfoLocationType(): DashboardInfoLocationType
  protected abstract getBreadcrumbNavigationLevel(): BreadcrumbNavigationLevel

  override ngOnInit(): void {
    super.ngOnInit()

    this.onRouteChangedSubscription = this.route.paramMap.pipe(
      tap(() => this.showSpinner = true),
      switchMap((paramMap: ParamMap) => {
        const location = paramMap.get(this.getParamKey()) as string
        const searchAthletesRequest = this.getSearchAthletesRequestDto(location)

        const dashboardRequest = new DashboardInfoRequestDto(DashboardInfoType.Athletes, this.getDashboardInfoLocationType(), location)
        const breadcrumbRequest = new BreadcrumbRequestDto(this.getBreadcrumbNavigationLevel(), location)

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