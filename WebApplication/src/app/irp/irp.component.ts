import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { tap, mergeMap, map } from 'rxjs/operators'
import { PaceWithTime } from '../_core/paceWithTime';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IrpIntervalResultComponent } from './irp-interval-result.component';
import { BracketRankComponent } from '../_subComponents/bracket-rank/bracket-rank.component';
import { BreadcrumbNavigationLevel, BreadcrumbRequestDto } from '../_core/breadcrumbRequestDto';
import { BreadcrumbComponent } from '../_common/breadcrumbComponent';
import { BreadcrumbLocation } from '../_common/breadcrumbLocation';
import { EventsBreadcrumbComponent } from '../_subComponents/breadcrumbs/events-bread-crumb/events-bread-crumb.component';
import { RouterModule } from '@angular/router';
import { LocationInfoRankingsComponent } from '../_subComponents/location-info-rankings/location-info-rankings.component';
import { IrpPizzaTrackerComponent } from './irp-pizza-tracker.component';
import { mapRaceSeriesTypeToImageUrl } from '../_common/IRaceSeriesType';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { config } from '../config';

@Component({
  standalone: true,
  templateUrl: './irp.component.html',
  imports: [IrpIntervalResultComponent, BracketRankComponent, EventsBreadcrumbComponent, LocationInfoRankingsComponent, IrpPizzaTrackerComponent, NgbModule, CommonModule, RouterModule],
  providers: [HttpClient],
  styleUrls: ['./irp.component.css']
})
export class IrpComponent extends BreadcrumbComponent implements OnInit {

  public getCourseStatistics(courseId: number): Observable<any> {
    return this.http.get<any>(`${config.apiUrl}/courseStatisticsApi/${courseId}`)
  }

  public getIrpCompetetors(athleteCourseId: number): Observable<any> {
    return this.http.get<any>(`${config.apiUrl}/irpCompetetorsApi/${athleteCourseId}`)
  }

  // extract as props to render in template.
  public athleteId!: number;
  public fullName!: string;
  public raceAge!: number;
  public genderAbbreviated!: string;
  public bib!: string;
  public paceWithTimeCumulative!: PaceWithTime;
  public raceSeriesImageUrl!: string;
  public firstName!: string;
  public raceName!: string;
  public courseId!: number;
  public courseName!: string;
  public courseDistance!: number;
  public timeZoneAbbreviated!: string;
  public finishTime!: string | null;
  public courseDate!: string;
  public courseTime!: string;
  public tags!: string[];
  public raceSeriesCity!: string;
  public raceSeriesState!: string;
  public raceSeriesDescription!: string;
  public trainingList!: string[];
  public courseGoalDescription!: string;
  public personalGoalDescription!: string;
  public locationInfoWithRank: any
  public bracketResults!: any[];
  public intervalResults!: any[];
  public competetorsForIrpDto: any
  public courseStatistics: any

  public dataLoaded = false
  public intervalPercentage!: number;

  constructor(route: ActivatedRoute, http: HttpClient) {
    super(route, http)
    this.breadcrumbLocation = BreadcrumbLocation.Irp
  }

  ngOnInit() {

    const athleteCourseId = this.getId()

    const $courseStats = this.getIrpDto(athleteCourseId).pipe(
      tap(this.setPropsToRender),
      mergeMap(() => this.getCourseStatistics(athleteCourseId))
    )

    $courseStats.subscribe((results: any) => {

      this.courseStatistics = results.find((oo: any) => !oo.bracketId);

      for (const bracket of this.bracketResults) {
        const statForBracket = results.find((oo: any) => oo.bracketId === bracket.id)
        bracket.averagePaceWithTime = statForBracket.averagePaceWithTime
        bracket.fastestPaceWithTime = statForBracket.fastestPaceWithTime
        bracket.slowestPaceWithTime = statForBracket.slowestPaceWithTime
      }
    })

    this.getIrpCompetetors(athleteCourseId).subscribe((competetorsResult: any) => {
      this.competetorsForIrpDto = competetorsResult
    })

    const breadcrumbRequest = new BreadcrumbRequestDto(BreadcrumbNavigationLevel.Irp, athleteCourseId.toString())
    this.setEventsBreadcrumbResult(breadcrumbRequest)
  }

  /**
   * this sets the properties needed on the front-end.
   */
  private setPropsToRender = (irpDto: any) => {
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

  public toggleBracketPopover = (popover: any, bracket: any) => {
    popover.isOpen() ? popover.close() : popover.open({bracket})
  }
}
