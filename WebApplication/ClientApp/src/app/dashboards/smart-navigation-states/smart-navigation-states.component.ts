import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { ComponentBaseWithRoutes } from 'src/app/_common/componentBaseWithRoutes';
import { DashboardInfoResponseDto, NavigationItem } from 'src/app/_orchestration/getDashboardInfo/dashboardInfoResponseDto';

@Component({
  selector: 'app-smart-navigation-states',
  templateUrl: './smart-navigation-states.component.html',
  styleUrls: ['./smart-navigation-states.component.css']
})
export class SmartNavigationStatesComponent extends ComponentBaseWithRoutes implements OnInit, OnChanges {

  @Input('dashboardInfoResponseDto')
  public dashboardInfoResponseDto: DashboardInfoResponseDto

  @Input('useEventsNavigation')
  public useEventsNavigation: boolean;

  public stateRoute: string

  // extract as props to render in template.
  public states: NavigationItem[]

  ngOnInit() {
    this.stateRoute = this.useEventsNavigation ? this.EventsStatePage : this.AthletesStatePage
  }

  ngOnChanges(_changes: SimpleChanges) {
    const {
      states,
    } = this.dashboardInfoResponseDto

    this.states = states
  }
}
