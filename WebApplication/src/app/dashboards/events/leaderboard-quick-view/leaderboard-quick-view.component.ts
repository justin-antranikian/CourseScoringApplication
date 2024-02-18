import { Component, inject } from '@angular/core';
import { NgbActiveModal, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ComponentBaseWithRoutes } from '../../../_common/componentBaseWithRoutes';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { LocationInfoRankingsComponent } from '../../../_subComponents/location-info-rankings/location-info-rankings.component';
import { LeaderboardResultComponent } from '../../../_subComponents/leaderboard-results-grid/leaderboard-result.component';

@Component({
  standalone: true,
  selector: 'ngbd-modal-content',
  templateUrl: './leaderboard-quick-view.component.html',
  imports: [CommonModule, RouterModule, LocationInfoRankingsComponent, NgbModule, LeaderboardResultComponent],
})
export class LeaderboardQuickViewModalContent extends ComponentBaseWithRoutes {
  modal = inject(NgbActiveModal)

  public raceLeaderboard: any | null
}
