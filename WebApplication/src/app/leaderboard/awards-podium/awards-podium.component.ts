import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink, RouterModule } from '@angular/router';
import { BreadcrumbComponent } from '../../_common/breadcrumbComponent';
import { mergeMap, tap } from 'rxjs/operators';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { BreadcrumbLocation } from '../../_common/breadcrumbLocation';
import { BreadcrumbRequestDto, BreadcrumbNavigationLevel } from '../../_core/getBreadcrumb/breadcrumbRequestDto';
import { Observable } from 'rxjs';
import { CommonModule } from '@angular/common';
import { config } from '../../config';

@Component({
  standalone: true,
  imports: [CommonModule, RouterModule, HttpClientModule],
  selector: 'app-awards-podium',
  templateUrl: './awards-podium.component.html',
  styleUrls: []
})
export class AwardsPodiumComponent extends BreadcrumbComponent implements OnInit {

  public getAwardsPodium(courseId: number): Observable<any> {
    return this.http.get<any>(`${config.apiUrl}/awardsPodiumApi/${courseId}`)
  }

  public awards: any[] = []
  public courseInfo: any
  public dataLoaded: any

  constructor(route: ActivatedRoute, http: HttpClient) {
    super(route, http)
    this.breadcrumbLocation = BreadcrumbLocation.CourseLeaderboard
  }

  ngOnInit() {
    const courseId = this.getId()

    const breadcrumbRequest = new BreadcrumbRequestDto(BreadcrumbNavigationLevel.CourseLeaderboard, courseId.toString())
    this.setEventsBreadcrumbResult(breadcrumbRequest)

    this.getCourseInfo(courseId)

    const $getCourseInfo = this.getAwardsPodium(courseId).pipe(
      tap((result: any) => this.awards = result),
      mergeMap(() => this.getCourseInfo(courseId))
    )

    $getCourseInfo.subscribe((result: any) => {
      this.courseInfo = result
      this.dataLoaded = true
    })
  }
}
