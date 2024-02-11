import { Component, Input, OnInit } from '@angular/core';
import { NgbModal, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { map, tap } from 'rxjs/operators';
import { EventSearchResultDto } from '../../../_core/eventSearchResultDto';
import { ComponentBaseWithRoutes } from '../../../_common/componentBaseWithRoutes';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { mapRaceSeriesTypeToImageUrl } from '../../../_common/IRaceSeriesType';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { LocationInfoRankingsComponent } from '../../../_subComponents/location-info-rankings/location-info-rankings.component';
import { LeaderboardResultComponent } from '../../../_subComponents/leaderboard-results-grid/leaderboard-result.component';
import { config } from '../../../config';

@Component({
  standalone: true,
  selector: 'app-event-search-result',
  templateUrl: './event-search-result.component.html',
  styleUrls: ['./event-search-result.component.css'],
  imports: [CommonModule, RouterModule, LocationInfoRankingsComponent, NgbModule, LeaderboardResultComponent]
})
export class EventSearchResultComponent extends ComponentBaseWithRoutes implements OnInit {

  public getRaceLeaderboard(raceId: number): Observable<any> {

    const getRaceLeaderboard$ = this.http.get<any>(`${config.apiUrl}/raceLeaderboardApi/${raceId}`).pipe(
      map((raceLeaderboardDto: any): any => ({
        ...mapRaceSeriesTypeToImageUrl(raceLeaderboardDto),
        leaderboards: this.mapIntervalTypeImages(raceLeaderboardDto.leaderboards)
      }))
    )

    return getRaceLeaderboard$
  }

  constructor(
    private http: HttpClient,
    private modalService: NgbModal
  ) { super() }

  @Input('eventSearchResult')
  public eventSearchResult!: EventSearchResultDto

  // extract as props to render in template.
  public raceSeriesImageUrl!: string
  public id!: number
  public name!: string
  public upcomingRaceId!: number
  public rating!: number

  // props for when view leaderboard is clicked.
  public raceName!: string
  public raceSeriesDescription!: string
  public raceKickOffDate!: string
  public locationInfoWithRank: any
  public leaderboards!: any[]

  ngOnInit() {
    const {
      raceSeriesImageUrl,
      id,
      name,
      upcomingRaceId,
      rating,
    } = this.eventSearchResult

    this.raceSeriesImageUrl = raceSeriesImageUrl as any
    this.id = id
    this.name = name
    this.upcomingRaceId = upcomingRaceId
    this.rating = rating
  }

  public onViewLeaderboardClicked = (modal: any) => {
    const raceId = this.eventSearchResult.upcomingRaceId
    const getLeaderboard$ = this.getRaceLeaderboard(raceId).pipe(
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
