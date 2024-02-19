import { Component, Input, OnInit } from '@angular/core';
import { ComponentBaseWithRoutes } from '../../_common/componentBaseWithRoutes';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  standalone: true,
  selector: 'app-location-info-rankings',
  templateUrl: './location-info-rankings.component.html',
  imports: [CommonModule, RouterModule],
  styleUrls: []
})
export class LocationInfoRankingsComponent extends ComponentBaseWithRoutes implements OnInit {

  @Input('locationInfoWithRank')
  public locationInfoWithRank: any;

  @Input('useEventsNavigation')
  public useEventsNavigation: boolean = false;

  public allRoute!: string;
  public areaRoute!: string;
  public cityRoute!: string;
  public stateRoute!: string;

  ngOnInit() {
    this.allRoute = this.useEventsNavigation ? this.EventsPage : this.AthletesPage
    this.stateRoute = this.useEventsNavigation ? this.EventsStatePage : this.AthletesStatePage
    this.areaRoute = this.useEventsNavigation ? this.EventsAreaPage : this.AthletesAreaPage
    this.cityRoute = this.useEventsNavigation ? this.EventsCityPage : this.AthletesCityPage
  }
}
