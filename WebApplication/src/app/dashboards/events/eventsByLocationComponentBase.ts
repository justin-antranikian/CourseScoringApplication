import { Injectable, OnDestroy, OnInit, inject } from "@angular/core";
import { ActivatedRoute, ParamMap } from "@angular/router";
import { switchMap, of, forkJoin, Subscription } from "rxjs";
import { EventsComponentBase } from "./eventsComponentBase";
import { BreadcrumbNavigationLevel, BreadcrumbRequestDto } from "../../_core/breadcrumbRequestDto";
import { DashboardInfoLocationType, DashboardInfoRequestDto, DashboardInfoType } from "../../_core/dashboardInfoRequestDto";
import { SearchEventsRequestDto } from "../../_core/searchEventsRequestDto";

@Injectable()
export abstract class EventsByLocationComponentBase extends EventsComponentBase implements OnInit, OnDestroy {

  private onRouteChangedSubscription: Subscription | null = null
  private route = inject(ActivatedRoute)

  protected abstract getParamKey(): any
  protected abstract getSearchEventsRequestDto(location: string): SearchEventsRequestDto
  protected abstract getDashboardInfoLocationType(): DashboardInfoLocationType
  protected abstract getBreadcrumbNavigationLevel(): BreadcrumbNavigationLevel

  override ngOnInit() {
    super.ngOnInit()

    this.onRouteChangedSubscription = this.route.paramMap.pipe(
      switchMap((paramMap: ParamMap) => {
        const location = paramMap.get(this.getParamKey()) as string
        const searchEventsRequest = this.getSearchEventsRequestDto(location)

        const dashboardRequest = new DashboardInfoRequestDto(DashboardInfoType.Events, this.getDashboardInfoLocationType(), location)
        const breadcrumbRequest = new BreadcrumbRequestDto(this.getBreadcrumbNavigationLevel(), location)

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
      this.eventSearchResults = data[1]
      this.eventsBreadcrumbResult = data[2]
      this.title = data[3]
    })
  }

  override ngOnDestroy() {
    super.ngOnDestroy()
    this.onRouteChangedSubscription!.unsubscribe();
  }
}