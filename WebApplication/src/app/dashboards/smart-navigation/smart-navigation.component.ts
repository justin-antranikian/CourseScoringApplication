import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { ComponentBaseWithRoutes } from '../../_common/componentBaseWithRoutes';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { SmartNavigationStatesComponent } from '../smart-navigation-states/smart-navigation-states.component';

@Component({
  standalone: true,
  selector: 'app-smart-navigation',
  templateUrl: './smart-navigation.component.html',
  imports: [CommonModule, RouterModule, SmartNavigationStatesComponent],
  styleUrls: ['./smart-navigation.component.css']
})
export class SmartNavigationComponent extends ComponentBaseWithRoutes implements OnInit, OnChanges {

  @Input('dashboardInfoResponseDto')
  public dashboardInfoResponseDto: any

  @Input('useEventsNavigation')
  public useEventsNavigation!: boolean;

  public areaRoute!: string
  public cityRoute!: string
  public stateRoute!: string

  // extract as props to render in template.
  public states!: any[]
  public areas!: any[]
  public stateNavigationItem!: any

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
