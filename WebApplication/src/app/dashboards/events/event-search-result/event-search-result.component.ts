import { Component, Input, OnInit } from '@angular/core';
import { NgbModal, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { EventSearchResultDto } from '../../../_core/eventSearchResultDto';
import { ComponentBaseWithRoutes } from '../../../_common/componentBaseWithRoutes';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { LocationInfoRankingsComponent } from '../../../_subComponents/location-info-rankings/location-info-rankings.component';
import { LeaderboardResultComponent } from '../../../_subComponents/leaderboard-results-grid/leaderboard-result.component';
import { ScoringApiService } from '../../../services/scoring-api.service';
import { Subscription } from 'rxjs';

@Component({
  standalone: true,
  selector: 'app-event-search-result',
  templateUrl: './event-search-result.component.html',
  styleUrls: ['./event-search-result.component.css'],
  imports: [CommonModule, RouterModule, LocationInfoRankingsComponent, NgbModule, LeaderboardResultComponent]
})
export class EventSearchResultComponent extends ComponentBaseWithRoutes implements OnInit {

  constructor(
    private modalService: NgbModal,
    private scoringApiService: ScoringApiService
  ) { super() }

  @Input('eventSearchResult')
  public eventSearchResult!: EventSearchResultDto

  public raceLeaderboard: any | null

  public subscription: Subscription | null = null

  ngOnInit() {
    
  }

  public onViewLeaderboardClicked = (modal: any) => {
    const raceId = this.eventSearchResult.upcomingRaceId

    this.scoringApiService.getRaceLeaderboard(raceId).subscribe((raceLeaderboardDto: any) => {
      this.raceLeaderboard = raceLeaderboardDto
      this.modalService.open(modal, { size: 'xl' });
    });
  }
}
