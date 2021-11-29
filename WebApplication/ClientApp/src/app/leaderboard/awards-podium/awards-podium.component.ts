import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiRequestService } from 'src/app/_services/api-request.service';
import { BreadcrumbComponent } from '../../_common/breadcrumbComponent';
import { BreadcrumbsApiRequestService } from 'src/app/_services/breadcrumbs-api-request.service';
import { BreadcrumbNavigationLevel, BreadcrumbRequestDto } from 'src/app/_orchestration/getBreadcrumb/breadcrumbRequestDto';
import { BreadcrumbLocation } from 'src/app/_common/breadcrumbLocation';
import { flatMap, tap } from 'rxjs/operators';

@Component({
  selector: 'app-awards-podium',
  templateUrl: './awards-podium.component.html',
  styleUrls: []
})
export class AwardsPodiumComponent extends BreadcrumbComponent implements OnInit {

  public awards: any[] = []
  public courseInfo: any
  public dataLoaded: boolean

  constructor(route: ActivatedRoute, apiService: ApiRequestService, breadcrumbApiService: BreadcrumbsApiRequestService) {
    super(route, apiService, breadcrumbApiService)
    this.breadcrumbLocation = BreadcrumbLocation.CourseLeaderboard
  }

  ngOnInit() {
    const courseId = this.getId()

    const breadcrumbRequest = new BreadcrumbRequestDto(BreadcrumbNavigationLevel.CourseLeaderboard, courseId.toString())
    this.setEventsBreadcrumbResult(breadcrumbRequest)

    this.apiService.getCourseInfo(courseId)

    const $getCourseInfo = this.apiService.getAwardsPodium(courseId).pipe(
      tap(result => this.awards = result),
      flatMap(() => this.apiService.getCourseInfo(courseId))
    )

    $getCourseInfo.subscribe(result => {
      this.courseInfo = result
      this.dataLoaded = true
    })
  }
}
