
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiRequestService } from '../_services/api-request.service';
import { BreadcrumbLocation } from '../_common/breadcrumbLocation';
import { BreadcrumbComponent } from '../_common/breadcrumbComponent';
import { BreadcrumbRequestDto, BreadcrumbNavigationLevel } from 'src/app/_orchestration/getBreadcrumb/breadcrumbRequestDto';
import { BreadcrumbsApiRequestService } from 'src/app/_services/breadcrumbs-api-request.service';
import { tap, flatMap } from 'rxjs/operators';

@Component({
  selector: 'app-irp-compare',
  templateUrl: './irp-compare.component.html',
  styleUrls: []
})
export class IrpCompareComponent extends BreadcrumbComponent implements OnInit {

  public athleteInfoDtos: any[]
  public courseInfo: any
  public dataLoaded = false
  public athleteCourseIds: string[]

  constructor(route: ActivatedRoute, apiService: ApiRequestService, breadcrumbApiService: BreadcrumbsApiRequestService) {
    super(route, apiService, breadcrumbApiService)
    this.breadcrumbLocation = BreadcrumbLocation.Irp

    const athleteCourseIdsAsString = (this.route.snapshot.queryParamMap as any).params["athleteCourseIds"] as string
    this.athleteCourseIds = JSON.parse(athleteCourseIdsAsString)
  }

  ngOnInit() {
    const courseId = this.getId()

    const breadcrumbRequest = new BreadcrumbRequestDto(BreadcrumbNavigationLevel.Irp, courseId.toString())
    this.setEventsBreadcrumbResult(breadcrumbRequest)

    const athleteCourseIds = this.athleteCourseIds.map(oo => parseInt(oo))

    const $getCourseInfo = this.apiService.getIrpToCompare(athleteCourseIds).pipe(
      tap(result => this.athleteInfoDtos = result),
      flatMap(() => this.apiService.getCourseInfo(courseId))
    )

    $getCourseInfo.subscribe(result => {
      this.courseInfo = result
      this.dataLoaded = true
    })
  }
}
