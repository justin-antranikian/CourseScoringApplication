import { Component, Input, OnInit } from '@angular/core';
import { ArpResultDto } from '../_orchestration/getArp/arpDto';
import { PaceWithTime } from '../_core/enums/paceWithTime';
import { ComponentBaseWithRoutes } from '../_common/componentBaseWithRoutes';

@Component({
  selector: '[app-arp-result]',
  templateUrl: './arp-result.component.html',
  styleUrls: []
})
export class ArpResultComponent extends ComponentBaseWithRoutes implements OnInit {

  @Input('arpResultDto')
  public arpResultDto: ArpResultDto

  // extract as props to render in template.
  public athleteCourseId: number
  public overallRank: number
  public overallCount: number
  public genderRank: number
  public genderCount: number
  public primaryDivisionRank: number
  public primaryDivisionCount: number
  public paceWithTimeCumulative: PaceWithTime
  public raceSeriesImageUrl: string
  public city: string
  public state: string
  public courseId: number
  public courseName: string
  public raceId: number
  public raceName: string

  ngOnInit() {
    const {
      athleteCourseId,
      overallRank,
      overallCount,
      genderRank,
      genderCount,
      primaryDivisionRank,
      primaryDivisionCount,
      paceWithTimeCumulative,
      raceSeriesImageUrl,
      city,
      state,
      raceId,
      raceName,
      courseId,
      courseName,
    } = this.arpResultDto

    this.athleteCourseId = athleteCourseId
    this.overallRank = overallRank
    this.overallCount = overallCount
    this.genderRank = genderRank
    this.genderCount = genderCount
    this.primaryDivisionRank = primaryDivisionRank
    this.primaryDivisionCount = primaryDivisionCount
    this.paceWithTimeCumulative = paceWithTimeCumulative
    this.raceSeriesImageUrl = raceSeriesImageUrl
    this.city = city
    this.state = state
    this.raceId = raceId
    this.raceName = raceName
    this.courseId = courseId
    this.courseName = courseName
  }
}
