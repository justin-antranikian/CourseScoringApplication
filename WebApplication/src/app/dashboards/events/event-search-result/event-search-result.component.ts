import { Component, Input } from '@angular/core';
import { NgbModal, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { tap } from 'rxjs/operators';
import { EventSearchResultDto } from '../../../_core/eventSearchResultDto';
import { ComponentBaseWithRoutes } from '../../../_common/componentBaseWithRoutes';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { LocationInfoRankingsComponent } from '../../../_subComponents/location-info-rankings/location-info-rankings.component';
import { LeaderboardResultComponent } from '../../../_subComponents/leaderboard-results-grid/leaderboard-result.component';
import { ScoringApiService } from '../../../services/scoring-api.service';

@Component({
  standalone: true,
  selector: 'app-event-search-result',
  templateUrl: './event-search-result.component.html',
  styleUrls: ['./event-search-result.component.css'],
  imports: [CommonModule, RouterModule, LocationInfoRankingsComponent, NgbModule, LeaderboardResultComponent]
})
export class EventSearchResultComponent extends ComponentBaseWithRoutes {

  constructor(
    private modalService: NgbModal,
    private scoringApiService: ScoringApiService
  ) { super() }

  @Input('eventSearchResult')
  public eventSearchResult!: EventSearchResultDto

  // props for when view leaderboard is clicked.
  public raceName!: string
  public raceSeriesDescription!: string
  public raceKickOffDate!: string
  public locationInfoWithRank: any
  public leaderboards!: any[]

  public onViewLeaderboardClicked = (modal: any) => {
    const raceId = this.eventSearchResult.upcomingRaceId
    const getLeaderboard$ = this.scoringApiService.getRaceLeaderboard(raceId).pipe(
      tap(this.setPropsToRender)
    )

    getLeaderboard$.subscribe((_raceLeaderboardDto: any) => {
      this.modalService.open(modal, { size: 'xl' });
    });
  }

  /**
  * this sets the properties of the RaceLeaderboardDto when user clicks to view more.
  */
  private setPropsToRender = (raceLeaderboardDto: any) => {
    const {
      raceName,
      raceSeriesDescription,
      raceKickOffDate,
      locationInfoWithRank,
      leaderboards,
    } = raceLeaderboardDto

    this.raceName = raceName
    this.raceSeriesDescription = raceSeriesDescription
    this.raceKickOffDate = raceKickOffDate
    this.locationInfoWithRank = locationInfoWithRank
    this.leaderboards = leaderboards
  }
}
