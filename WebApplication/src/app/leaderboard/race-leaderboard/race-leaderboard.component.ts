import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { BreadcrumbLocation } from '../../_common/breadcrumbLocation';
import { BreadcrumbComponent } from '../../_common/breadcrumbComponent';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { BreadcrumbNavigationLevel, BreadcrumbRequestDto } from '../../_core/breadcrumbRequestDto';
import { EventsBreadcrumbComponent } from '../../_subComponents/breadcrumbs/events-bread-crumb/events-bread-crumb.component';
import { LocationInfoRankingsComponent } from '../../_subComponents/location-info-rankings/location-info-rankings.component';
import { LeaderboardResultComponent } from '../../_subComponents/leaderboard-results-grid/leaderboard-result.component';
import { IrpsSearchComponent } from '../../_subComponents/irp-search/irps-search.component';

@Component({
  standalone: true,
  selector: 'app-race-leaderboard',
  templateUrl: './race-leaderboard.component.html',
  imports: [HttpClientModule, CommonModule, RouterModule, EventsBreadcrumbComponent, LocationInfoRankingsComponent, LeaderboardResultComponent, IrpsSearchComponent],
  styleUrls: ['./race-leaderboard.component.css']
})
export class RaceLeaderboardComponent extends BreadcrumbComponent implements OnInit {

  public raceId!: number
  public dataLoaded = false

  // extract as props to render in template.
  public raceSeriesImageUrl!: string
  public raceName!: string
  public raceSeriesDescription!: string
  public raceKickOffDate!: string
  public locationInfoWithRank: any
  public leaderboards!: any[]

  constructor(route: ActivatedRoute, http: HttpClient) {
    super(route, http)
    this.breadcrumbLocation = BreadcrumbLocation.RaceLeaderboard
  }

  ngOnInit() {
    this.raceId = this.getId()
    this.getRaceLeaderboard(this.raceId).subscribe(this.setPropsToRender)

    const breadcrumbRequest = new BreadcrumbRequestDto(BreadcrumbNavigationLevel.RaceLeaderboard, this.raceId.toString())
    this.setEventsBreadcrumbResult(breadcrumbRequest)
  }

  /**
  * this sets the properties needed on the front-end.
  */
  private setPropsToRender = (raceLeaderboardDto: any) => {
    const {
      raceSeriesImageUrl,
      raceName,
      raceSeriesDescription,
      raceKickOffDate,
      locationInfoWithRank,
      leaderboards,
    } = raceLeaderboardDto

    this.raceSeriesImageUrl = raceSeriesImageUrl
    this.raceName = raceName
    this.raceSeriesDescription = raceSeriesDescription
    this.raceKickOffDate = raceKickOffDate
    this.locationInfoWithRank = locationInfoWithRank
    this.leaderboards = leaderboards

    this.dataLoaded = true
  }
}
