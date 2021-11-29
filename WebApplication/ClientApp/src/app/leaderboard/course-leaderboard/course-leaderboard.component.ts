import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiRequestService } from 'src/app/_services/api-request.service';
import { CourseLeaderboardByIntervalDto, CourseLeaderboardDto, CourseMetadata } from '../../_orchestration/getLeaderboard/getCourseLeaderboard/courseLeaderboardDto';
import { BracketType } from '../../_core/enums/bracketType';
import { BreadcrumbLocation } from '../../_common/breadcrumbLocation';
import { removeUndefinedKeyValues } from '../../_common/jsonHelpers';
import { CourseLeaderboardFilter } from './courseLeaderboardFilter';
import { BreadcrumbComponent } from '../../_common/breadcrumbComponent';
import { LocationInfoWithRank } from '../../_orchestration/locationInfoWithRank';
import { BreadcrumbRequestDto, BreadcrumbNavigationLevel } from 'src/app/_orchestration/getBreadcrumb/breadcrumbRequestDto';
import { BreadcrumbsApiRequestService } from 'src/app/_services/breadcrumbs-api-request.service';
import { LeaderboardResultDto } from 'src/app/_orchestration/getLeaderboard/leaderboardResultDto';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { IrpDto } from 'src/app/_orchestration/getIrp/irpDto';

@Component({
  selector: 'app-course-leaderboard',
  templateUrl: './course-leaderboard.component.html',
  styleUrls: ['./course-leaderboard.component.css']
})
export class CourseLeaderboardComponent extends BreadcrumbComponent implements OnInit {

  public resultFilters: CourseLeaderboardFilter

  // extract as props to render in template.
  public raceSeriesImageUrl: string
  public raceName: string
  public raceSeriesDescription: string
  public locationInfoWithRank: LocationInfoWithRank
  public courseName: string
  public courseDate: string
  public courseTime: string
  public courseDistance: number
  public courseMetadata: CourseMetadata
  public leaderboards: CourseLeaderboardByIntervalDto[]
  public selectedIrp: IrpDto

  public courseId: number
  public athleteCourseIdsToCompare: number[] = []
  public athleteCourseIdsToCompareString: string = null

  public dataLoaded = false

  constructor(
    route: ActivatedRoute,
    apiService: ApiRequestService,
    breadcrumbApiService: BreadcrumbsApiRequestService,
    private readonly modalService: NgbModal,
    private readonly router: Router,
  ) {
    super(route, apiService, breadcrumbApiService)
    this.breadcrumbLocation = BreadcrumbLocation.CourseLeaderboard
  }

  ngOnInit() {

    this.route.params.subscribe(() => {
      this.dataLoaded = false
      this.initData()
    });

    this.route.queryParams.subscribe((data: CourseLeaderboardFilter) => {
      const { bracketId, intervalId } = data

      // this condition is when the course changes which is handled by the params.subscribe()
      if (!bracketId && !intervalId) {
        return
      }

      const courseId = this.getId()
      this.resultFilters = new CourseLeaderboardFilter(courseId, bracketId, intervalId)
      const getLeaderboard$ = this.apiService.getCourseLeaderboard(this.getId(), this.resultFilters)
      getLeaderboard$.subscribe((course: CourseLeaderboardDto) => this.handleFiltersChange(course, intervalId, bracketId))
    })
  }

  /**
  * update the fields from the courseLeaderboardDto. No need to update the breadcrumbs.
  */
  private handleFiltersChange = (courseLeaderboardDto: CourseLeaderboardDto, intervalId: number, bracketId: number) => {
    this.resultFilters.intervalId = parseInt(intervalId.toString())
    this.resultFilters.bracketId = parseInt(bracketId.toString())
    this.setPropsToRender(courseLeaderboardDto)
  }

  /**
  * this sets the properties needed on the front-end.
  */
  private setPropsToRender = (courseLeaderboardDto: CourseLeaderboardDto) => {
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
    const getLeaderboard$ = this.apiService.getCourseLeaderboard(courseId)
    
    getLeaderboard$.subscribe((courseLeaderboardDto: CourseLeaderboardDto) => {
      const overallBracket = courseLeaderboardDto.courseMetadata.brackets.find(oo => oo.bracketType == BracketType.Overall)
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
      intervalId: parseInt(intervalId.toString()) === 0 ? null : intervalId
    })

    this.router.navigate([], { relativeTo: this.route, queryParams: params });
  }

  public onCourseChanged = () => {
    this.athleteCourseIdsToCompare = []
    this.athleteCourseIdsToCompareString = null
    this.router.navigate([this.CourseLeaderboardPage, this.resultFilters.courseId])
  }

  public onViewIrpClicked = (modal, result: LeaderboardResultDto) => {
    this.apiService.getIrpDto(result.athleteCourseId).subscribe((irpDto: IrpDto) => {
      this.selectedIrp = irpDto
      this.modalService.open(modal, { size: 'xl' });
    })
  }

  public compareIrpClicked = ({ athleteCourseId }: LeaderboardResultDto) => {
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
