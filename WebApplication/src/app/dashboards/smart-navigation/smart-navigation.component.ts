import { Component, Input, OnInit } from '@angular/core';
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
export class SmartNavigationComponent extends ComponentBaseWithRoutes implements OnInit {

  @Input('dashboardInfoResponseDto')
  public dashboardInfoResponseDto: any

  @Input('useEventsNavigation')
  public useEventsNavigation!: boolean;

  public areaRoute!: string
  public cityRoute!: string
  public stateRoute!: string

  ngOnInit() {
    this.areaRoute = this.useEventsNavigation ? this.EventsAreaPage : this.AthletesAreaPage
    this.cityRoute = this.useEventsNavigation ? this.EventsCityPage : this.AthletesCityPage
    this.stateRoute = this.useEventsNavigation ? this.EventsStatePage : this.AthletesStatePage
  }
}
