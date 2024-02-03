import { Component, Input, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ComponentBaseWithRoutes } from '../../_common/componentBaseWithRoutes';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { mapRaceSeriesTypeToImageUrl } from '../../_common/IRaceSeriesType';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { IrpQuickViewComponent } from './irp-quick-view.component';
import { config } from '../../config';

@Component({
  standalone: true,
  selector: '[app-leaderboard-result]',
  templateUrl: './leaderboard-result.component.html',
  imports: [CommonModule, RouterModule, IrpQuickViewComponent],
  styleUrls: []
})
export class LeaderboardResultComponent extends ComponentBaseWithRoutes implements OnInit {

  public getIrpDto(athleteCourseId: number): Observable<any> {

    const getIrpDto$ = this.http.get<any>(`${config.apiUrl}/irpApi/${athleteCourseId}`).pipe(
      map(mapRaceSeriesTypeToImageUrl),
      map((irpDto: any): any => ({
        ...irpDto,
        intervalResults: this.mapIntervalTypeImages(irpDto.intervalResults)
      }))
    )

    return getIrpDto$
  }

  constructor(
    private http: HttpClient,
    private modalService: NgbModal
  ) { super() }

  @Input('leaderboardResult')
  public leaderboardResult: any

  @Input('hideViewIrpButton')
  public hideViewIrpButton!: boolean

  // extract as props to render in template.
  public athleteId!: number
  public fullName!: string
  public raceAge!: number
  public genderAbbreviated!: string
  public bib!: string
  public paceWithTimeCumulative: any
  public athleteCourseId!: number
  public overallRank!: number
  public genderRank!: number
  public divisionRank!: number

  // props for when view irp is clicked.
  public raceName!: string
  public courseId!: number
  public timeZoneAbbreviated!: string
  public finishTime!: string | null
  public tags!: string[]
  public locationInfoWithRank: any
  public bracketResults!: any[]
  public intervalResults!: any[]
  public selectedIrp: any = null

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

    this.getIrpDto(athleteCourseId).subscribe((irpDto: any) => {
      this.selectedIrp = irpDto
      this.modalService.open(modal, { size: 'xl' });
    })
  }
}
