import { ActivatedRoute } from "@angular/router";
import { ApiRequestService } from "../_services/api-request.service";
import { BreadcrumbRequestDto } from "../_orchestration/getBreadcrumb/breadcrumbRequestDto";
import { BreadcrumbResultDto } from "../_orchestration/getBreadcrumb/breadcrumbResultDto";
import { BreadcrumbsApiRequestService } from "../_services/breadcrumbs-api-request.service";
import { EventsBreadcrumbResultDto } from "../_orchestration/getBreadcrumb/eventsBreadcrumbResultDto";
import { BreadcrumbLocation } from "./breadcrumbLocation";
import { ComponentBaseWithRoutes } from "./componentBaseWithRoutes";

export abstract class BreadcrumbComponent extends ComponentBaseWithRoutes {

  public breadcrumbLocation: BreadcrumbLocation
  public eventsBreadcrumbResult: EventsBreadcrumbResultDto
  public athletesBreadcrumbResult: BreadcrumbResultDto

  constructor(
    protected readonly route: ActivatedRoute,
    protected readonly apiService: ApiRequestService,
    protected readonly breadcrumbApiService: BreadcrumbsApiRequestService
  ) { super() }

  protected getId = () => parseInt(this.route.snapshot.paramMap.get('id'))

  /**
   * calls api and sets athletesBreadcrumbResult property.
   * @param breadcrumbRequestDto 
   */
  protected setAthletesBreadcrumbResult = (breadcrumbRequestDto: BreadcrumbRequestDto) => {

    const getBreadcrumbs$ = this.breadcrumbApiService.getAthletesBreadCrumbsResult(breadcrumbRequestDto)

    getBreadcrumbs$.subscribe((breadcrumbResult: BreadcrumbResultDto) => {
      this.athletesBreadcrumbResult = breadcrumbResult
    })
  }

  /**
  * calls api and sets eventsBreadcrumbResult property.
  * @param BreadcrumbRequestDto
  */
  protected setEventsBreadcrumbResult = (breadcrumbRequestDto: BreadcrumbRequestDto) => {

    const getBreadcrumbs$ = this.breadcrumbApiService.getEventsBreadCrumbsResult(breadcrumbRequestDto)

    getBreadcrumbs$.subscribe((breadcrumbResultDto: EventsBreadcrumbResultDto) => {
      this.eventsBreadcrumbResult = breadcrumbResultDto
    })
  }
}