import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { BreadcrumbLocation } from '../_common/breadcrumbLocation';
import { BreadcrumbComponent } from '../_common/breadcrumbComponent';
import { tap, mergeMap } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { BreadcrumbNavigationLevel, BreadcrumbRequestDto } from '../_core/getBreadcrumb/breadcrumbRequestDto';
import { Observable } from 'rxjs';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  imports: [CommonModule, RouterModule],
  selector: 'app-irp-compare',
  templateUrl: './irp-compare.component.html',
  styleUrls: []
})
export class IrpCompareComponent extends BreadcrumbComponent implements OnInit {

  public getIrpToCompare(athleteCourseIds: number[]): Observable<any> {
    const body: any = {
      AthleteCourseIds: athleteCourseIds
    };

    return this.http.post<any>(`https://localhost:44308/compareIrpApi`, body)
  }

  public athleteInfoDtos!: any[]
  public courseInfo: any
  public dataLoaded = false
  public athleteCourseIds: string[]

  constructor(route: ActivatedRoute, http: HttpClient) {
    super(route, http)
    this.breadcrumbLocation = BreadcrumbLocation.Irp

    const athleteCourseIdsAsString = (this.route.snapshot.queryParamMap as any).params["athleteCourseIds"] as string
    this.athleteCourseIds = JSON.parse(athleteCourseIdsAsString)
  }

  ngOnInit() {
    const courseId = this.getId()

    const breadcrumbRequest = new BreadcrumbRequestDto(BreadcrumbNavigationLevel.Irp, courseId.toString())
    this.setEventsBreadcrumbResult(breadcrumbRequest)

    const athleteCourseIds = this.athleteCourseIds.map(oo => parseInt(oo))

    const $getCourseInfo = this.getIrpToCompare(athleteCourseIds).pipe(
      tap(result => this.athleteInfoDtos = result),
      mergeMap(() => this.getCourseInfo(courseId))
    )

    $getCourseInfo.subscribe(result => {
      this.courseInfo = result
      this.dataLoaded = true
    })
  }
}