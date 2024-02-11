import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
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
export class SmartNavigationStatesComponent extends ComponentBaseWithRoutes implements OnInit, OnChanges {

  @Input('dashboardInfoResponseDto')
  public dashboardInfoResponseDto: any

  @Input('useEventsNavigation')
  public useEventsNavigation!: boolean;

  public stateRoute!: string

  // extract as props to render in template.
  public states!: any[]

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
