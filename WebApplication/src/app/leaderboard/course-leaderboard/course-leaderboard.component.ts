import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { BreadcrumbLocation } from '../../_common/breadcrumbLocation';
import { NgbToastModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpClientModule } from '@angular/common/http';
import { EventsBreadcrumbComponent } from '../../_subComponents/breadcrumbs/events-bread-crumb/events-bread-crumb.component';
import { LocationInfoRankingsComponent } from '../../_subComponents/location-info-rankings/location-info-rankings.component';
import { CommonModule } from '@angular/common';
import { IrpsSearchComponent } from '../irp-search/irps-search.component';
import { FormsModule } from '@angular/forms';
import { Observable } from 'rxjs';
import { BreadcrumbNavigationLevel, BreadcrumbRequestDto } from '../../_core/breadcrumbRequestDto';
import { LeaderboardComponentBase } from '../leaderboardComponentBase';
import { IRaceSeriesType } from '../../_common/IRaceSeriesType';
import { IntervalType } from '../../_core/intervalType';
import { LeaderboardResultDto } from '../leaderboardResultDto';

export interface CourseLeaderboardDto extends IRaceSeriesType {
  courseDate: string
  courseDistance: number
  courseName: string
  courseTime: string
  leaderboards: CourseLeaderboardByIntervalDto[]
  locationInfoWithRank: any
  raceId: number
  raceName: string
  raceSeriesId: number
  raceSeriesDescription: string
}

interface CourseLeaderboardByIntervalDto {
  intervalName: string
  intervalType: IntervalType
  results: LeaderboardResultDto[]
  totalRacers: number
}

@Component({
  standalone: true,
  selector: 'app-course-leaderboard',
  imports: [EventsBreadcrumbComponent, LocationInfoRankingsComponent, RouterLink, NgbToastModule, HttpClientModule, CommonModule, IrpsSearchComponent, FormsModule],
  templateUrl: './course-leaderboard.component.html',
  styleUrls: ['./course-leaderboard.component.css']
})
export class CourseLeaderboardComponent extends LeaderboardComponentBase implements OnInit {

  public courseId!: number
  public courseLeaderboard$!: Observable<CourseLeaderboardDto>
  public eventsBreadcrumbResult$!: Observable<any>

  public selectedIrp: any
  public showToast = false;

  public athleteCourseIdsToCompare: number[] = []
  public athleteCourseIdsToCompareString: any = null

  constructor(private route: ActivatedRoute) {
    super()
    this.breadcrumbLocation = BreadcrumbLocation.CourseLeaderboard
  }

  override ngOnInit() {
    super.ngOnInit()

    const courseId = parseInt(this.route.snapshot.paramMap.get('id') as any)
    this.courseId = courseId
    this.courseLeaderboard$ = this.scoringApiService.getCourseLeaderboard(courseId)

    const breadcrumbRequest = new BreadcrumbRequestDto(BreadcrumbNavigationLevel.CourseLeaderboard, courseId.toString())
    this.eventsBreadcrumbResult$ = this.scoringApiService.getEventsBreadCrumbsResult(breadcrumbRequest)
  }

  public compareIrpClicked = ({ athleteCourseId }: any) => {
    const athleteCourseIds = this.athleteCourseIdsToCompare

    if (!athleteCourseIds.includes(athleteCourseId)) {
      athleteCourseIds.push(athleteCourseId)
    } else {
      const index = athleteCourseIds.indexOf(athleteCourseId);
      athleteCourseIds.splice(index, 1);
    }

    this.athleteCourseIdsToCompareString = JSON.stringify(athleteCourseIds)
    this.showToast = true
  }

  public onCloseCompareCliecked = () => {
    this.athleteCourseIdsToCompare = []
    this.showToast = false
  }
}
