import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiRequestService } from 'src/app/_services/api-request.service';
import { RaceLeaderboardByCourseDto, RaceLeaderboardDto } from '../../_orchestration/getLeaderboard/getRaceLeaderboard/raceLeaderboardDto';
import { BreadcrumbLocation } from '../../_common/breadcrumbLocation';
import { BreadcrumbComponent } from '../../_common/breadcrumbComponent';
import { LocationInfoWithRank } from '../../_orchestration/locationInfoWithRank';
import { BreadcrumbRequestDto, BreadcrumbNavigationLevel } from 'src/app/_orchestration/getBreadcrumb/breadcrumbRequestDto';
import { BreadcrumbsApiRequestService } from 'src/app/_services/breadcrumbs-api-request.service';

@Component({
  selector: 'app-race-leaderboard',
  templateUrl: './race-leaderboard.component.html',
  styleUrls: ['./race-leaderboard.component.css']
})
export class RaceLeaderboardComponent extends BreadcrumbComponent implements OnInit {

  public raceId: number
  public dataLoaded = false

  // extract as props to render in template.
  public raceSeriesImageUrl: string
  public raceName: string
  public raceSeriesDescription: string
  public raceKickOffDate: string
  public locationInfoWithRank: LocationInfoWithRank
  public leaderboards: RaceLeaderboardByCourseDto[]

  constructor(route: ActivatedRoute, apiService: ApiRequestService, breadcrumbApiService: BreadcrumbsApiRequestService) {
    super(route, apiService, breadcrumbApiService)
    this.breadcrumbLocation = BreadcrumbLocation.RaceLeaderboard
  }

  ngOnInit() {
    this.raceId = this.getId()
    this.apiService.getRaceLeaderboard(this.raceId).subscribe(this.setPropsToRender)

    const breadcrumbRequest = new BreadcrumbRequestDto(BreadcrumbNavigationLevel.RaceLeaderboard, this.raceId.toString())
    this.setEventsBreadcrumbResult(breadcrumbRequest)
  }

  /**
  * this sets the properties needed on the front-end.
  */
  private setPropsToRender = (raceLeaderboardDto: RaceLeaderboardDto) => {
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
