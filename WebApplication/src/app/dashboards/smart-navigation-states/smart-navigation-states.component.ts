import { Component, Input, OnInit } from '@angular/core';
import { ComponentBaseWithRoutes } from '../../_common/componentBaseWithRoutes';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  standalone: true,
  selector: 'app-smart-navigation-states',
  templateUrl: './smart-navigation-states.component.html',
  imports: [CommonModule, RouterModule],
  styleUrls: ['./smart-navigation-states.component.css']
})
export class SmartNavigationStatesComponent extends ComponentBaseWithRoutes implements OnInit {

  @Input('dashboardInfoResponseDto')
  public dashboardInfoResponseDto: any

  @Input('useEventsNavigation')
  public useEventsNavigation!: boolean;

  public stateRoute!: string

  ngOnInit() {
    this.stateRoute = this.useEventsNavigation ? this.EventsStatePage : this.AthletesStatePage
  }
}
