import { Component, Input, OnInit } from '@angular/core';
import { NgbModal, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { tap } from 'rxjs/operators';
import { ComponentBaseWithRoutes } from '../../../_common/componentBaseWithRoutes';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { AthleteSearchResultDto } from '../../../_core/athleteSearchResultDto';
import { LocationInfoRankingsComponent } from '../../../_subComponents/location-info-rankings/location-info-rankings.component';
import { BracketRankComponent } from '../../../_subComponents/bracket-rank/bracket-rank.component';
import { IntervalTimeComponent } from '../../../_subComponents/interval-time/interval-time.component';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ScoringApiService } from '../../../services/scoring-api.service';

@Component({
  standalone: true,
  selector: 'athlete-search-result',
  templateUrl: './athlete-search-result.component.html',
  imports: [HttpClientModule, RouterModule, CommonModule, NgbModule, LocationInfoRankingsComponent, BracketRankComponent, IntervalTimeComponent],
  styleUrls: []
})
export class AthleteSearchResultComponent extends ComponentBaseWithRoutes {

  constructor(
    private http: HttpClient,
    private modalService: NgbModal,
    private scoringApiService: ScoringApiService
  ) { super() }

  @Input('athleteSearchResult')
  public athleteSearchResult!: AthleteSearchResultDto;

  // props for when view arp is clicked.
  public age!: number
  public genderAbbreviated!: string
  public results!: any[]
  public tags!: string[]
  public allEventsGoal: any

  public onViewArpClicked = (athleteInfoModal: any) => {
    const athleteId = this.athleteSearchResult.id

    const getArpDto$ = this.scoringApiService.getArpDto(athleteId).pipe(
      tap(this.setPropsToRender)
    )

    // TODO this there should be only one subsciption.
    getArpDto$.subscribe((_arpDto: any) => {
      this.modalService.open(athleteInfoModal, { size: 'xl' });
    })
  }

  /**
  * this sets the properties of the ArpDto when user clicks to view more.
  */
  private setPropsToRender = (arpDto: any) => {
    const {
      age,
      genderAbbreviated,
      results,
      tags,
      allEventsGoal,
    } = arpDto

    this.age = age
    this.genderAbbreviated = genderAbbreviated
    this.results = results
    this.tags = tags
    this.allEventsGoal = allEventsGoal
  }
}
