import { Component, Input, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ComponentBaseWithRoutes } from 'src/app/_common/componentBaseWithRoutes';
import { RaceLeaderboardByCourseDto, RaceLeaderboardDto } from 'src/app/_orchestration/getLeaderboard/getRaceLeaderboard/raceLeaderboardDto';
import { EventSearchResultDto } from 'src/app/_orchestration/searchEvents/eventSearchResultDto';
import { ApiRequestService } from 'src/app/_services/api-request.service';
import { tap } from 'rxjs/operators';
import { LocationInfoWithRank } from 'src/app/_orchestration/locationInfoWithRank';

@Component({
  selector: 'app-event-search-result',
  templateUrl: './event-search-result.component.html',
  styleUrls: ['./event-search-result.component.css']
})
export class EventSearchResultComponent extends ComponentBaseWithRoutes implements OnInit {

  constructor(
    private apiService: ApiRequestService,
    private modalService: NgbModal
  ) { super() }

  @Input('eventSearchResult')
  public eventSearchResult: EventSearchResultDto

  // extract as props to render in template.
  public raceSeriesImageUrl: string
  public id: number
  public name: string
  public upcomingRaceId: number
  public rating: number

  // props for when view leaderboard is clicked.
  public raceName: string
  public raceSeriesDescription: string
  public raceKickOffDate: string
  public locationInfoWithRank: LocationInfoWithRank
  public leaderboards: RaceLeaderboardByCourseDto[]

  ngOnInit() {
    const {
      raceSeriesImageUrl,
      id,
      name,
      upcomingRaceId,
      rating,
    } = this.eventSearchResult

    this.raceSeriesImageUrl = raceSeriesImageUrl
    this.id = id
    this.name = name
    this.upcomingRaceId = upcomingRaceId
    this.rating = rating
  }

  public onViewLeaderboardClicked = (modal) => {
    const raceId = this.eventSearchResult.upcomingRaceId
    const getLeaderboard$ = this.apiService.getRaceLeaderboard(raceId).pipe(
      tap(this.setPropsToRender)
    )

    getLeaderboard$.subscribe((_raceLeaderboardDto: RaceLeaderboardDto) => {
      this.modalService.open(modal, { size: 'xl' });
    });
  }

  /**
  * this sets the properties of the RaceLeaderboardDto when user clicks to view more.
  */
  private setPropsToRender = (raceLeaderboardDto: RaceLeaderboardDto) => {
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
