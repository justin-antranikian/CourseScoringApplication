import { Component, Input, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ComponentBaseWithRoutes } from 'src/app/_common/componentBaseWithRoutes';
import { PaceWithTime } from 'src/app/_core/enums/paceWithTime';
import { IrpDto, IrpResultByBracketDto, IrpResultByIntervalDto } from 'src/app/_orchestration/getIrp/irpDto';
import { LeaderboardResultDto } from 'src/app/_orchestration/getLeaderboard/leaderboardResultDto';
import { LocationInfoWithRank } from 'src/app/_orchestration/locationInfoWithRank';
import { ApiRequestService } from 'src/app/_services/api-request.service';

@Component({
  selector: '[app-leaderboard-result]',
  templateUrl: './leaderboard-result.component.html',
  styleUrls: []
})
export class LeaderboardResultComponent extends ComponentBaseWithRoutes implements OnInit {

  constructor(
    private apiService: ApiRequestService,
    private modalService: NgbModal
  ) { super() }

  @Input('leaderboardResult')
  public leaderboardResult: LeaderboardResultDto

  @Input('hideViewIrpButton')
  public hideViewIrpButton: boolean

  // extract as props to render in template.
  public athleteId: number
  public fullName: string
  public raceAge: number
  public genderAbbreviated: string
  public bib: string
  public paceWithTimeCumulative: PaceWithTime
  public athleteCourseId: number
  public overallRank: number
  public genderRank: number
  public divisionRank: number

  // props for when view irp is clicked.
  public raceName: string
  public courseId: number
  public timeZoneAbbreviated: string
  public finishTime: string | null
  public tags: string[]
  public locationInfoWithRank: LocationInfoWithRank
  public bracketResults: IrpResultByBracketDto[]
  public intervalResults: IrpResultByIntervalDto[]
  public selectedIrp: IrpDto = null

  ngOnInit() {
    const {
      athleteId,
      fullName,
      raceAge,
      genderAbbreviated,
      bib,
      paceWithTimeCumulative,
      athleteCourseId,
      overallRank,
      genderRank,
      divisionRank,
    } = this.leaderboardResult

    this.athleteId = athleteId
    this.fullName = fullName
    this.raceAge = raceAge
    this.genderAbbreviated = genderAbbreviated
    this.bib = bib
    this.paceWithTimeCumulative = paceWithTimeCumulative
    this.athleteCourseId = athleteCourseId
    this.overallRank = overallRank
    this.genderRank = genderRank
    this.divisionRank = divisionRank
  }

  public onViewIrpClicked = (modal: any) => {
    const { athleteCourseId } = this.leaderboardResult

    this.apiService.getIrpDto(athleteCourseId).subscribe((irpDto: IrpDto) => {
      this.selectedIrp = irpDto
      this.modalService.open(modal, { size: 'xl' });
    })
  }
}
