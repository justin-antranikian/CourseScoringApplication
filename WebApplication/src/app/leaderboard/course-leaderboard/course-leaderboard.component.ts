import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { BracketType } from '../../_core/enums/bracketType';
import { BreadcrumbLocation } from '../../_common/breadcrumbLocation';
import { removeUndefinedKeyValues } from '../../_common/jsonHelpers';
import { CourseLeaderboardFilter } from './courseLeaderboardFilter';
import { BreadcrumbComponent } from '../../_common/breadcrumbComponent';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { BreadcrumbNavigationLevel, BreadcrumbRequestDto } from '../../_core/getBreadcrumb/breadcrumbRequestDto';
import { EventsBreadcrumbComponent } from '../../_subComponents/breadcrumbs/events-bread-crumb/events-bread-crumb.component';
import { LocationInfoRankingsComponent } from '../../_subComponents/location-info-rankings/location-info-rankings.component';
import { IrpQuickViewComponent } from '../../_subComponents/leaderboard-results-grid/irp-quick-view.component';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  selector: 'app-course-leaderboard',
  imports: [EventsBreadcrumbComponent, LocationInfoRankingsComponent, RouterLink, IrpQuickViewComponent, HttpClientModule, CommonModule],
  templateUrl: './course-leaderboard.component.html',
  styleUrls: ['./course-leaderboard.component.css']
})
export class CourseLeaderboardComponent extends BreadcrumbComponent implements OnInit {

  public resultFilters!: CourseLeaderboardFilter

  // extract as props to render in template.
  public raceSeriesImageUrl!: string
  public raceName!: string
  public raceSeriesDescription!: string
  public locationInfoWithRank: any
  public courseName!: string
  public courseDate!: string
  public courseTime!: string
  public courseDistance!: number
  public courseMetadata: any
  public leaderboards!: any[]
  public selectedIrp: any

  public courseId!: number
  public athleteCourseIdsToCompare: number[] = []
  public athleteCourseIdsToCompareString: any = null

  public dataLoaded = false

  constructor(
    route: ActivatedRoute,
    http: HttpClient,
    private readonly modalService: NgbModal,
    private readonly router: Router,
  ) {
    super(route, http)
    this.breadcrumbLocation = BreadcrumbLocation.CourseLeaderboard
  }

  ngOnInit() {

    this.route.params.subscribe(() => {
      this.dataLoaded = false
      this.initData()
    });

    this.route.queryParams.subscribe((data: any) => {
      const { bracketId, intervalId } = data

      // this condition is when the course changes which is handled by the params.subscribe()
      if (!bracketId && !intervalId) {
        return
      }

      const courseId = this.getId()
      this.resultFilters = new CourseLeaderboardFilter(courseId, bracketId, intervalId)
      const getLeaderboard$ = this.getCourseLeaderboard(this.getId(), this.resultFilters)
      getLeaderboard$.subscribe((course: any) => this.handleFiltersChange(course, intervalId, bracketId))
    })
  }

  /**
  * update the fields from the courseLeaderboardDto. No need to update the breadcrumbs.
  */
  private handleFiltersChange = (courseLeaderboardDto: any, intervalId: number, bracketId: number) => {
    this.resultFilters.intervalId = parseInt(intervalId.toString())
    this.resultFilters.bracketId = parseInt(bracketId.toString())
    this.setPropsToRender(courseLeaderboardDto)
  }

  /**
  * this sets the properties needed on the front-end.
  */
  private setPropsToRender = (courseLeaderboardDto: any) => {
    const {
      raceSeriesImageUrl,
      raceName,
      raceSeriesDescription,
      locationInfoWithRank,
      courseName,
      courseDate,
      courseTime,
      courseDistance,
      courseMetadata,
      leaderboards,
    } = courseLeaderboardDto

    this.raceSeriesImageUrl = raceSeriesImageUrl
    this.raceName = raceName
    this.raceSeriesDescription = raceSeriesDescription
    this.locationInfoWithRank = locationInfoWithRank
    this.courseName = courseName
    this.courseDate = courseDate
    this.courseTime = courseTime
    this.courseDistance = courseDistance
    this.courseMetadata = courseMetadata
    this.leaderboards = leaderboards

    this.dataLoaded = true
  }

  private initData = () => {
    const courseId = this.getId()
    this.courseId = courseId
    const getLeaderboard$ = this.getCourseLeaderboard(courseId)

    getLeaderboard$.subscribe((courseLeaderboardDto: any) => {
      const overallBracket = courseLeaderboardDto.courseMetadata.brackets.find((oo: any) => oo.bracketType == BracketType.Overall)
      this.resultFilters = new CourseLeaderboardFilter(courseId, overallBracket.id, 0)
      this.setPropsToRender(courseLeaderboardDto)
    })

    const breadcrumbRequest = new BreadcrumbRequestDto(BreadcrumbNavigationLevel.CourseLeaderboard, courseId.toString())
    this.setEventsBreadcrumbResult(breadcrumbRequest)
  }

  public onFilterChanged = () => {
    const { bracketId, intervalId } = this.resultFilters

    const params = removeUndefinedKeyValues({
      bracketId,
      intervalId: parseInt((intervalId as any).toString()) === 0 ? null : intervalId
    })

    this.router.navigate([], { relativeTo: this.route, queryParams: params });
  }

  public onCourseChanged = () => {
    this.athleteCourseIdsToCompare = []
    this.athleteCourseIdsToCompareString = null
    this.router.navigate([this.CourseLeaderboardPage, this.resultFilters.courseId])
  }

  public onViewIrpClicked = (modal: any, result: any) => {
    this.getIrpDto(result.athleteCourseId).subscribe((irpDto: any) => {
      this.selectedIrp = irpDto
      this.modalService.open(modal, { size: 'xl' });
    })
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
  }
}
