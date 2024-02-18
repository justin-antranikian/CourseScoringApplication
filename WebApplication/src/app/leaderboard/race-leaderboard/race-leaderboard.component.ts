import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { BreadcrumbLocation } from '../../_common/breadcrumbLocation';
import { BreadcrumbComponent } from '../../_common/breadcrumbComponent';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { BreadcrumbNavigationLevel, BreadcrumbRequestDto } from '../../_core/breadcrumbRequestDto';
import { EventsBreadcrumbComponent } from '../../_subComponents/breadcrumbs/events-bread-crumb/events-bread-crumb.component';
import { LocationInfoRankingsComponent } from '../../_subComponents/location-info-rankings/location-info-rankings.component';
import { LeaderboardResultComponent } from '../../_subComponents/leaderboard-results-grid/leaderboard-result.component';
import { IrpsSearchComponent } from '../../_subComponents/irp-search/irps-search.component';
import { ScoringApiService } from '../../services/scoring-api.service';
import { Observable } from 'rxjs';

@Component({
  standalone: true,
  selector: 'app-race-leaderboard',
  templateUrl: './race-leaderboard.component.html',
  imports: [HttpClientModule, CommonModule, RouterModule, EventsBreadcrumbComponent, LocationInfoRankingsComponent, LeaderboardResultComponent, IrpsSearchComponent],
  styleUrls: ['./race-leaderboard.component.css']
})
export class RaceLeaderboardComponent extends BreadcrumbComponent implements OnInit {

  public raceId!: number
  public race$!: Observable<any>

  constructor(private route: ActivatedRoute, private scoringApiService: ScoringApiService) {
    super()
    this.breadcrumbLocation = BreadcrumbLocation.RaceLeaderboard
  }

  ngOnInit() {
    this.raceId = parseInt(this.route.snapshot.paramMap.get('id') as any)
    this.race$ = this.scoringApiService.getRaceLeaderboard(this.raceId)

    const breadcrumbRequest = new BreadcrumbRequestDto(BreadcrumbNavigationLevel.RaceLeaderboard, this.raceId.toString())
    this.scoringApiService.getEventsBreadCrumbsResult(breadcrumbRequest).subscribe(result => {
      this.eventsBreadcrumbResult = result
    })
  }
}
