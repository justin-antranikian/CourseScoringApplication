import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { ComponentBaseWithRoutes } from 'src/app/_common/componentBaseWithRoutes';
import { DashboardInfoResponseDto, NavigationItem } from 'src/app/_orchestration/getDashboardInfo/dashboardInfoResponseDto';

@Component({
  selector: 'app-smart-navigation',
  templateUrl: './smart-navigation.component.html',
  styleUrls: ['./smart-navigation.component.css']
})
export class SmartNavigationComponent extends ComponentBaseWithRoutes implements OnInit, OnChanges {

  @Input('dashboardInfoResponseDto')
  public dashboardInfoResponseDto: DashboardInfoResponseDto

  @Input('useEventsNavigation')
  public useEventsNavigation: boolean

  public areaRoute: string
  public cityRoute: string
  public stateRoute: string

  // extract as props to render in template.
  public states: NavigationItem[]
  public areas: NavigationItem[]
  public stateNavigationItem: NavigationItem

  ngOnInit() {
    this.areaRoute = this.useEventsNavigation ? this.EventsAreaPage : this.AthletesAreaPage
    this.cityRoute = this.useEventsNavigation ? this.EventsCityPage : this.AthletesCityPage
    this.stateRoute = this.useEventsNavigation ? this.EventsStatePage : this.AthletesStatePage
  }

  ngOnChanges(_changes: SimpleChanges) {
    const {
      states,
      areas,
      stateNavigationItem
    } = this.dashboardInfoResponseDto

    this.states = states
    this.areas = areas
    this.stateNavigationItem = stateNavigationItem
  }
}
