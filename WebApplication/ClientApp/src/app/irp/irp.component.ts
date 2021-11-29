import { tap, flatMap } from 'rxjs/operators'

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IrpDto, IrpResultByBracketDto, IrpResultByIntervalDto } from '../_orchestration/getIrp/irpDto';
import { ApiRequestService } from '../_services/api-request.service';
import { BreadcrumbLocation } from '../_common/breadcrumbLocation';
import { BreadcrumbComponent } from '../_common/breadcrumbComponent';
import { ChartOptionsForIrp } from './chartOptionsForIrp';
import { PaceWithTime } from '../_core/enums/paceWithTime';
import { LocationInfoWithRank } from '../_orchestration/locationInfoWithRank';
import { BreadcrumbRequestDto, BreadcrumbNavigationLevel } from 'src/app/_orchestration/getBreadcrumb/breadcrumbRequestDto';
import { BreadcrumbsApiRequestService } from 'src/app/_services/breadcrumbs-api-request.service';
import { GetCompetetorsForIrpDto } from '../_orchestration/getCompetetorsForIrp/GetCompetetorsForIrpDto';

@Component({
  selector: 'app-irp',
  templateUrl: './irp.component.html',
  styleUrls: ['./irp.component.css']
})
export class IrpComponent extends BreadcrumbComponent implements OnInit {

  public chartOptions = new ChartOptionsForIrp();

  // extract as props to render in template.
  public athleteId: number
  public fullName: string
  public raceAge: number
  public genderAbbreviated: string
  public bib: string
  public paceWithTimeCumulative: PaceWithTime
  public raceSeriesImageUrl: string
  public firstName: string
  public raceName: string
  public courseId: number
  public courseName: string
  public courseDistance: number
  public timeZoneAbbreviated: string
  public finishTime: string | null
  public courseDate: string
  public courseTime: string
  public tags: string[]
  public raceSeriesCity: string
  public raceSeriesState: string
  public raceSeriesDescription: string
  public trainingList: string[]
  public courseGoalDescription: string
  public personalGoalDescription: string
  public locationInfoWithRank: LocationInfoWithRank
  public bracketResults: IrpResultByBracketDto[]
  public intervalResults: IrpResultByIntervalDto[]
  public competetorsForIrpDto: GetCompetetorsForIrpDto
  public courseStatistics: any

  public dataLoaded = false
  public intervalPercentage: number

  constructor(route: ActivatedRoute, apiService: ApiRequestService, breadcrumbApiService: BreadcrumbsApiRequestService) {
    super(route, apiService, breadcrumbApiService)
    this.breadcrumbLocation = BreadcrumbLocation.Irp
  }

  ngOnInit() {
    const athleteCourseId = this.getId()

    const $courseStats = this.apiService.getIrpDto(athleteCourseId).pipe(
      tap(this.setPropsToRender),
      flatMap(() => this.apiService.getCourseStatistics(athleteCourseId))
    )

    $courseStats.subscribe((results: any) => {

      this.courseStatistics = results.find(oo => !oo.bracketId);

      for (const bracket of this.bracketResults) {
        const statForBracket = results.find(oo => oo.bracketId === bracket.id)
        bracket.averagePaceWithTime = statForBracket.averagePaceWithTime
        bracket.fastestPaceWithTime = statForBracket.fastestPaceWithTime
        bracket.slowestPaceWithTime = statForBracket.slowestPaceWithTime
      }
    })

    this.apiService.getIrpCompetetors(athleteCourseId).subscribe((competetorsResult: GetCompetetorsForIrpDto) => {
      this.competetorsForIrpDto = competetorsResult
    })

    const breadcrumbRequest = new BreadcrumbRequestDto(BreadcrumbNavigationLevel.Irp, athleteCourseId.toString())
    this.setEventsBreadcrumbResult(breadcrumbRequest)
  }

  /**
   * this sets the properties needed on the front-end.
   */
  private setPropsToRender = (irpDto: IrpDto) => {
    const {
      athleteId,
      fullName,
      raceAge,
      genderAbbreviated,
      bib,
      paceWithTimeCumulative,
      raceSeriesImageUrl,
      firstName,
      raceName,
      courseId,
      courseName,
      courseDistance,
      timeZoneAbbreviated,
      finishTime,
      courseDate,
      courseTime,
      tags,
      raceSeriesCity,
      raceSeriesState,
      raceSeriesDescription,
      trainingList,
      courseGoalDescription,
      personalGoalDescription,
      locationInfoWithRank,
      bracketResults,
      intervalResults,
    } = irpDto

    this.athleteId = athleteId
    this.fullName = fullName
    this.raceAge = raceAge
    this.genderAbbreviated = genderAbbreviated
    this.bib = bib
    this.paceWithTimeCumulative = paceWithTimeCumulative
    this.raceSeriesImageUrl = raceSeriesImageUrl
    this.firstName = firstName
    this.raceName = raceName
    this.courseId = courseId
    this.courseName = courseName
    this.courseDistance = courseDistance
    this.timeZoneAbbreviated = timeZoneAbbreviated
    this.finishTime = finishTime
    this.courseDate = courseDate
    this.courseTime = courseTime
    this.tags = tags
    this.raceSeriesCity = raceSeriesCity
    this.raceSeriesState = raceSeriesState
    this.raceSeriesDescription = raceSeriesDescription
    this.trainingList = trainingList
    this.courseGoalDescription = courseGoalDescription
    this.personalGoalDescription = personalGoalDescription
    this.locationInfoWithRank = locationInfoWithRank
    this.bracketResults = bracketResults
    this.intervalResults = intervalResults
    this.intervalPercentage = 100 / irpDto.intervalResults.length

    this.dataLoaded = true
  }

  public toggleBracketPopover = (popover, bracket: any) => {
    popover.isOpen() ? popover.close() : popover.open({bracket})
  }
}
