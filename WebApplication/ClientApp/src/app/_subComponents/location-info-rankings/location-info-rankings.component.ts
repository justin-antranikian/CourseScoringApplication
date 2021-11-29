import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { ComponentBaseWithRoutes } from 'src/app/_common/componentBaseWithRoutes';
import { LocationInfoWithRank } from 'src/app/_orchestration/locationInfoWithRank';

@Component({
  selector: 'app-location-info-rankings',
  templateUrl: './location-info-rankings.component.html',
  styleUrls: []
})
export class LocationInfoRankingsComponent extends ComponentBaseWithRoutes implements OnInit, OnChanges {

  @Input('locationInfoWithRank')
  public locationInfoWithRank: LocationInfoWithRank;

  @Input('useEventsNavigation')
  public useEventsNavigation: boolean

  public allRoute: string
  public areaRoute: string
  public cityRoute: string
  public stateRoute: string

  // extract as props to render in template.
  public state: string
  public stateUrl: string
  public area: string
  public areaUrl: string
  public city: string
  public cityUrl: string
  public overallRank: number
  public stateRank: number
  public areaRank: number
  public cityRank: number

  ngOnInit() {
    this.allRoute = this.useEventsNavigation ? this.EventsPage : this.AthletesPage
    this.stateRoute = this.useEventsNavigation ? this.EventsStatePage : this.AthletesStatePage
    this.areaRoute = this.useEventsNavigation ? this.EventsAreaPage : this.AthletesAreaPage
    this.cityRoute = this.useEventsNavigation ? this.EventsCityPage : this.AthletesCityPage
  }

  ngOnChanges(_changes: SimpleChanges): void {
    const {
      state,
      stateUrl,
      area,
      areaUrl,
      city,
      cityUrl,
      overallRank,
      stateRank,
      areaRank,
      cityRank
    } = this.locationInfoWithRank

    this.state = state
    this.stateUrl = stateUrl
    this.area = area
    this.areaUrl = areaUrl
    this.city = city
    this.cityUrl = cityUrl
    this.overallRank = overallRank
    this.stateRank = stateRank
    this.areaRank = areaRank
    this.cityRank = cityRank
  }
}
