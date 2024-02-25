import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { BreadcrumbLocation } from '../../_common/breadcrumbLocation';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { BreadcrumbNavigationLevel, BreadcrumbRequestDto } from '../../_core/breadcrumbRequestDto';
import { EventsBreadcrumbComponent } from '../../_subComponents/breadcrumbs/events-bread-crumb/events-bread-crumb.component';
import { LocationInfoRankingsComponent } from '../../_subComponents/location-info-rankings/location-info-rankings.component';
import { IrpsSearchComponent } from '../irp-search/irps-search.component';
import { Observable } from 'rxjs';
import { LeaderboardComponentBase } from '../leaderboardComponentBase';
import { IRaceSeriesType } from '../../_common/IRaceSeriesType';

interface RaceLeaderboardDto extends IRaceSeriesType {
  leaderboards: any[]
  locationInfoWithRank: any
  raceKickOffDate: string
  raceName: string
  raceSeriesDescription: string
}

@Component({
  standalone: true,
  selector: 'app-race-leaderboard',
  templateUrl: './race-leaderboard.component.html',
  imports: [HttpClientModule, CommonModule, RouterModule, EventsBreadcrumbComponent, LocationInfoRankingsComponent, IrpsSearchComponent],
  styleUrls: ['./race-leaderboard.component.css']
})
export class RaceLeaderboardComponent extends LeaderboardComponentBase implements OnInit {

  public raceId!: number
  public raceLeaderboard$!: Observable<RaceLeaderboardDto>
  public eventsBreadcrumbResult$!: Observable<any>

  constructor(private route: ActivatedRoute) {
    super()
    this.breadcrumbLocation = BreadcrumbLocation.RaceLeaderboard
  }

  override ngOnInit() {
    super.ngOnInit()

    this.raceId = parseInt(this.route.snapshot.paramMap.get('id') as any)
    this.raceLeaderboard$ = this.scoringApiService.getRaceLeaderboard(this.raceId)

    const breadcrumbRequest = new BreadcrumbRequestDto(BreadcrumbNavigationLevel.RaceLeaderboard, this.raceId.toString())
    this.eventsBreadcrumbResult$ = this.scoringApiService.getEventsBreadCrumbsResult(breadcrumbRequest)
  }
}
