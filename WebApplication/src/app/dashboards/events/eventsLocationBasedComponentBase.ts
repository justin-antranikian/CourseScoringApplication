import { HttpClient } from "@angular/common/http";
import { Injectable, OnDestroy, OnInit } from "@angular/core";
import { ActivatedRoute, ParamMap } from "@angular/router";
import { switchMap, of, forkJoin, Subscription } from "rxjs";
import { ScoringApiService } from "../../services/scoring-api.service";
import { EventsComponentBase } from "./eventsComponentBase";
import { BreadcrumbRequestDto } from "../../_core/breadcrumbRequestDto";
import { DashboardInfoRequestDto } from "../../_core/dashboardInfoRequestDto";
import { SearchEventsRequestDto } from "../../_core/searchEventsRequestDto";

@Injectable()
export abstract class EventsLocationBasedComponentBase extends EventsComponentBase implements OnInit, OnDestroy {

  private onRouteChangedSubscription: Subscription | null = null

  protected constructor(route: ActivatedRoute, httpClient: HttpClient, scoringApiService: ScoringApiService) {
    super(route, httpClient, scoringApiService)
  }

  abstract getParamKey(): any
  abstract getDashboardInfoRequestDto(location: string): DashboardInfoRequestDto
  abstract getSearchEventsRequestDto(location: string): SearchEventsRequestDto
  abstract getBreadcrumbRequestDto(location: string): BreadcrumbRequestDto

  override ngOnInit(): void {
    super.ngOnInit()

    this.onRouteChangedSubscription = this.route.paramMap.pipe(
      switchMap((paramMap: ParamMap) => {
        const location = paramMap.get(this.getParamKey()) as string
        const dashboardRequest = this.getDashboardInfoRequestDto(location)
        const searchEventsRequest = this.getSearchEventsRequestDto(location)
        const breadcrumbRequest = this.getBreadcrumbRequestDto(location)

        const observables$ = [
          this.scoringApiService.getDashboardInfo(dashboardRequest),
          this.scoringApiService.getRaceSeriesResultsChunked(searchEventsRequest),
          this.scoringApiService.getAthletesBreadCrumbsResult(breadcrumbRequest),
          of(location)
        ]

        return forkJoin(observables$)
      })
    ).subscribe(data => {
      this.dashboardInfoResponseDto = data[0]
      this.eventSearchResultsChunked = data[1]
      this.eventsBreadcrumbResult = data[2]
      this.title = data[3]
    })
  }

  override ngOnDestroy() {
    super.ngOnDestroy()
    this.onRouteChangedSubscription?.unsubscribe();
  }
}